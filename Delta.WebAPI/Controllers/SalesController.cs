using Delta.Application.Repositories.SalesRepository;
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

    [HttpPost("upload")]
    public async Task<IActionResult> UploadSales(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        await _saleRepository.UploadSalesAsync(file);

        return Ok("Sales uploaded successfully.");
    }
}