using AutoMapper;
using JAS.ECommerce.Application.DTOs.Product;
using JAS.ECommerce.Domain.Entities;

namespace JAS.ECommerce.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductDto, Product>();
    }
}
