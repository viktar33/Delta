using Delta.Domain;
using Delta.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Delta.Application.Repositories.SalesRepository;
public class SalesRepository : ISalesRepository
{
    private readonly ApplicationDbContext context;

    public SalesRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task UploadSalesAsync(IFormFile file)
    {
        try
        {
            ValidateFileName(file.FileName, out var managerName, out var uploadDate);

            var sales = await ParseSalesFromFileAsync(file, managerName, uploadDate);

            await context.AddRangeAsync(sales);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error uploading sales data: " + ex.Message, ex);
        }
    }

    private void ValidateFileName(string fileName, out string managerName, out DateTime uploadDateUtc)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var parts = fileNameWithoutExtension.Split('_');

        if (parts.Length < 2 || !TryParseDate(parts[1], out uploadDateUtc))
        {
            throw new Exception("Invalid file name format. Expected format: ManagerName_ddMMyyyy.");
        }

        managerName = parts[0];
    }

    private async Task<List<Sale>> ParseSalesFromFileAsync(IFormFile file, string managerName, DateTime uploadDate)
    {
        var sales = new List<Sale>();

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var saleRecord = ParseSaleRecord(line, managerName, uploadDate);

                if (saleRecord != null)
                {
                    sales.Add(saleRecord);
                }
            }
        }

        return sales;
    }

    private Sale ParseSaleRecord(string line, string managerName, DateTime uploadDate)
    {
        var values = line.Split(',');

        if (values.Length != 4 || !TryParseDate(values[0], out var date))
        {
            return null;
        }

        return new Sale
        {
            ManagerName = managerName,
            UploadDate = uploadDate,
            Date = date,
            Client = values[1],
            Product = values[2],
            Amount = decimal.Parse(values[3], CultureInfo.InvariantCulture)
        };
    }

    private bool TryParseDate(string dateString, out DateTime dateUtc)
    {
        if (DateTime.TryParseExact(dateString, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var localDate))
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            dateUtc = TimeZoneInfo.ConvertTimeToUtc(localDate, localZone);
            return true;
        }

        dateUtc = default;
        return false;
    }
}