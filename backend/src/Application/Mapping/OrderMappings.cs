using AutoMapper;
using JAS.ECommerce.Application.DTOs.Order;
using JAS.ECommerce.Domain.Entities;

namespace JAS.ECommerce.Application.Mapping;

public partial class MappingProfile
{
    private void ConfigureOrderMappings()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderDto, Order>();
    }
}
