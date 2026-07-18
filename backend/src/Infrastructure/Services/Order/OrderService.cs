namespace JAS.ECommerce.Infrastructure.Services.Order;

using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Order;
using JAS.ECommerce.Application.Interfaces;
using JAS.ECommerce.Domain.Entities;
using JAS.ECommerce.Domain.Enums;
using JAS.ECommerce.Domain.Interfaces;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        IUnitOfWork unitOfWork,
        IInventoryService inventoryService,
        ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _inventoryService = inventoryService;
        _logger = logger;
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        try
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            return order != null ? MapToDto(order) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting order");
            throw;
        }
    }

    public async Task<PaginatedResponse<OrderDto>> GetUserOrdersAsync(int userId, PaginationDto pagination)
    {
        try
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            var userOrders = orders.Where(o => o.UserId == userId).ToList();
            var totalCount = userOrders.Count;
            var paginatedOrders = userOrders
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            return new PaginatedResponse<OrderDto>
            {
                Items = paginatedOrders.Select(MapToDto),
                TotalCount = totalCount,
                Page = pagination.Page,
                PageSize = pagination.PageSize
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user orders");
            throw;
        }
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        try
        {
            var order = new Order
            {
                OrderNumber = GenerateOrderNumber(),
                UserId = createOrderDto.UserId,
                ShippingAddressId = createOrderDto.ShippingAddressId,
                Status = OrderStatus.Pending,
                ShippingMethod = createOrderDto.ShippingMethod,
                ShippingFee = createOrderDto.ShippingFee,
                SubTotal = 0,
                TaxAmount = 0,
                DiscountAmount = 0,
                TotalAmount = 0,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Order created: {order.OrderNumber}");
            return MapToDto(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            throw;
        }
    }

    public async Task<OrderDto> UpdateOrderStatusAsync(int orderId, int status)
    {
        try
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId)
                ?? throw new KeyNotFoundException($"Order with ID {orderId} not found");

            order.Status = (OrderStatus)status;
            order.UpdatedAt = DateTime.UtcNow;

            if (status == (int)OrderStatus.Shipped)
            {
                order.ShippedAt = DateTime.UtcNow;
            }
            else if (status == (int)OrderStatus.Delivered)
            {
                order.DeliveredAt = DateTime.UtcNow;
            }

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Order status updated: {order.OrderNumber}");
            return MapToDto(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order status");
            throw;
        }
    }

    public async Task<OrderDto?> CancelOrderAsync(int orderId)
    {
        try
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;

            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Order cancelled: {order.OrderNumber}");
            return MapToDto(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling order");
            throw;
        }
    }

    private static string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            UserId = order.UserId,
            Status = (int)order.Status,
            SubTotal = order.SubTotal,
            ShippingFee = order.ShippingFee,
            TaxAmount = order.TaxAmount,
            DiscountAmount = order.DiscountAmount,
            TotalAmount = order.TotalAmount,
            ShippingMethod = order.ShippingMethod,
            TrackingNumber = order.TrackingNumber,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };
    }
}
