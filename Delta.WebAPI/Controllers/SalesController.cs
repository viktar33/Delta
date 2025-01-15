using Delta.Application.Dtos;
using Delta.Application.Repositories.SalesRepository;
using Delta.Application.RequestHelpers;
using Microsoft.AspNetCore.Mvc;

namespace Delta.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISalesRepository _saleRepository;

    public SalesController(ISalesRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    [HttpGet]
    public async Task<List<SaleDto>> GetSales([FromQuery] SalesParams saleParams)
    {
        return await _saleRepository.GetSales(saleParams);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadSales(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        await _saleRepository.UploadSalesAsync(file);

        return Ok("Sales uploaded successfully.");
    }
}