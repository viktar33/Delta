using Delta.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Delta.Application.Repositories.SalesRepository;

public interface ISalesRepository
{
    Task UploadSalesAsync(IFormFile file);
}
