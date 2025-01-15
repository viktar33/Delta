using Delta.Application.Dtos;
using Delta.Application.RequestHelpers;
using Delta.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Delta.Application.Repositories.SalesRepository;

public interface ISalesRepository
{
    Task<List<SaleDto>> GetSales(SalesParams salesParams);
    Task UploadSalesAsync(IFormFile file);
}
