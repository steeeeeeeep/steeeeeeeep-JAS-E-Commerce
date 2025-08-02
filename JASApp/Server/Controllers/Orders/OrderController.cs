using JAS.Shared.Dto.Order;
using JASApi.Data;
using JASData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JASApp.Api.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrderController(AppDbContext dbContext) : Controller
{
    protected readonly AppDbContext _dbContext = dbContext;

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrdersAsync()
    {
        try
        {
            List<Order> orders = await _dbContext.Orders.Where(c => !c.IsDeleted).ToListAsync();
            List<OrderDto> orderListDto = orders.Select(model => new OrderDto
            {
                Id = model.Id,
                ApplicationUserId = model.ApplicationUserId ?? 0,
                TotalAmount = model.TotalAmount,
                PaymentMethod = model.PaymentMethod,
                ShippingAddress = model.ShippingAddress,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Status = model.Status,
                OrderItems = model.OrderItems.Select(items => new OrderItemDto
                {
                    Id = items.Id,
                    OrderId = items.OrderId,
                    ProductId = items.ProductId,
                    Quantity = items.Quantity,
                    UnitPrice = items.UnitPrice,
                    TotalPrice = items.TotalPrice,
                }).ToList(),
            }).ToList();


            return orderListDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(int id)
    {
        try
        {
            Order model = await _dbContext.Orders.Where(c => c.Id == id).FirstOrDefaultAsync();
            OrderDto orderDto = new OrderDto
            {
                Id = model.Id,
                ApplicationUserId = model.ApplicationUserId ?? 0,
                TotalAmount = model.TotalAmount,
                PaymentMethod = model.PaymentMethod,
                ShippingAddress = model.ShippingAddress,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,
                Country = model.Country,
                Status = model.Status,
                OrderItems = model.OrderItems.Select(items => new OrderItemDto
                {
                    Id = items.Id,
                    OrderId = items.OrderId,
                    ProductId = items.ProductId,
                    Quantity = items.Quantity,
                    UnitPrice = items.UnitPrice,
                    TotalPrice = items.TotalPrice,
                }).ToList(),
            };


            return orderDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateOrderAsync(AddOrUpdateOrderDto orderDto)
    {
        try
        {
            if (orderDto.Id != 0)
                return false;

            Order order = new()
            {
                ApplicationUserId = orderDto.ApplicationUserId,
                TotalAmount = orderDto.TotalAmount,
                PaymentMethod = orderDto.PaymentMethod,
                ShippingAddress = orderDto.ShippingAddress,
                City = orderDto.City,
                State = orderDto.State,
                PostalCode = orderDto.PostalCode,
                Country = orderDto.Country,
                Status = orderDto.Status,
                CreatedOn = orderDto.CreatedOn,
            };

            await _dbContext.AddAsync(order);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("Update/{id}")]
    public async Task<ActionResult<bool>> UpdateOrderByIdAsync(int id, AddOrUpdateOrderDto updateOrder)
    {
        try
        {
            Order model = await _dbContext.Orders.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (model == null)
                return false;

            model.ApplicationUserId = updateOrder.ApplicationUserId;
            model.TotalAmount = updateOrder.TotalAmount;
            model.PaymentMethod = updateOrder.PaymentMethod;
            model.ShippingAddress = updateOrder.ShippingAddress;
            model.City = updateOrder.City;
            model.State = updateOrder.State;
            model.PostalCode = updateOrder.PostalCode;
            model.Country = updateOrder.Country;
            model.Status = updateOrder.Status;

            _dbContext.Orders.Update(model);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("Delete/{id}")]
    public async Task<ActionResult<bool>> DeleteOrderByIdAsync(int id)
    {
        try
        {
            Order model = await _dbContext.Orders.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (model == null)
                return false;

            model.DeletedOn = DateTime.UtcNow;
            model.IsDeleted = true;

            _dbContext.Orders.Update(model);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("Item/All")]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAllOrderItemsAsync()
    {
        try
        {
            List<OrderItem> orders = await _dbContext.OrderItems.Where(c => !c.IsDeleted).ToListAsync();
            List<OrderItemDto> orderListDto = orders.Select(items => new OrderItemDto
            {
                Id = items.Id,
                OrderId = items.OrderId,
                ProductId = items.ProductId,
                Quantity = items.Quantity,
                UnitPrice = items.UnitPrice,
                TotalPrice = items.TotalPrice,
            }).ToList();


            return orderListDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Item/Get/{id}")]
    public async Task<ActionResult<OrderItemDto>> GetOrderItemByIdAsync(int id)
    {
        try
        {
            OrderItem model = await _dbContext.OrderItems.Where(c => c.Id == id).FirstOrDefaultAsync();
            OrderItemDto orderDto = new OrderItemDto
            {
                Id = model.Id,
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,
                TotalPrice = model.TotalPrice,
            };


            return orderDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Item")]
    public async Task<ActionResult<bool>> CreateOrderItemAsync(AddOrUpdateOrderItemDto orderItemDto)
    {
        try
        {
            if (orderItemDto.Id != 0)
                return false;

            OrderItem order = new OrderItem()
            {
                Id = orderItemDto.Id,
                OrderId = orderItemDto.OrderId,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                UnitPrice = orderItemDto.UnitPrice,
                TotalPrice = orderItemDto.TotalPrice,
                CreatedOn = DateTime.UtcNow,
            };

            await _dbContext.OrderItems.AddAsync(order);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("Item/Update/{id}")]
    public async Task<ActionResult<bool>> UpdateOrderItemByIdAsync(int id, OrderItemDto updateOrderItemDto)
    {
        try
        {
            OrderItem model = await _dbContext.OrderItems.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (model == null)
                return false;

            model.Id = updateOrderItemDto.Id;
            model.OrderId = updateOrderItemDto.OrderId;
            model.ProductId = updateOrderItemDto.ProductId;
            model.Quantity = updateOrderItemDto.Quantity;
            model.UnitPrice = updateOrderItemDto.UnitPrice;
            model.TotalPrice = updateOrderItemDto.TotalPrice;

            _dbContext.OrderItems.Update(model);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("Item/Delete/{id}")]
    public async Task<ActionResult<bool>> DeleteOrderItemByIdAsync(int id)
    {
        try
        {
            OrderItem model = await _dbContext.OrderItems.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (model == null)
                return false;

            model.DeletedOn = DateTime.UtcNow;
            model.IsDeleted = true;

            _dbContext.OrderItems.Update(model);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
