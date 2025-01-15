using Delta.Application.Dtos;
using Delta.Domain.Models;
using Mapster;

namespace Delta.Application.MappingConfigs;

public class SaleMapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Sale, SaleDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ManagerName, src => src.ManagerName)
            .Map(dest => dest.UploadDate, src => src.UploadDate)
            .Map(dest => dest.Client, src => src.Client)
            .Map(dest => dest.Product, src => src.Product)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Date, src => src.Date);
    }
}