using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Order;

namespace JAS.ECommerce.Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<PaginatedResponse<OrderDto>> GetUserOrdersAsync(int userId, PaginationDto pagination);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<OrderDto> UpdateOrderStatusAsync(int orderId, int status);
    Task<OrderDto?> CancelOrderAsync(int orderId);
}
