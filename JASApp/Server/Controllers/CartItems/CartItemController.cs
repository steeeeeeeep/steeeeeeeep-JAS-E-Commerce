using JAS.Shared.Dto.CartItem;
using JASApp.Api.Data;
using JASData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JASApp.Api.Controllers.CartItems;

[Route("api/[controller]")]
[ApiController]
public class CartItemController(AppDbContext context) : Controller
{
    private readonly AppDbContext _dbContext = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAllCartItemsAsync()
    {
        try
        {
            List<CartItem> CartItems = await _dbContext.CartItems.Where(c => !c.IsDeleted).ToListAsync();

            if (CartItems.IsNullOrEmpty())
                return new List<CartItemDto>();

            List<CartItemDto> CartItemDtos = CartItems.Select(c => new CartItemDto()
            {
                Id = c.Id,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                AddedOn = DateTime.UtcNow,
            }).ToList();


            return CartItemDtos;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartItemDto>> GetCartItemByIdAsync(int id)
    {
        try
        {
            CartItem CartItem = await _dbContext.CartItems.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (CartItem == null)
                return new CartItemDto();

            CartItemDto CartItemDto = new CartItemDto()
            {
                Id = CartItem.Id,
                ProductId = CartItem.ProductId,
                Quantity = CartItem.Quantity,
                AddedOn = DateTime.UtcNow,
            };


            return CartItemDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateCartItemAsync(AddOrUpdateCartItemDto dto)
    {
        try
        {
            CartItem getCartItem = await _dbContext.CartItems.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (getCartItem != null)
                return false;

            CartItem CartItem = new CartItem()
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                AddedOn = dto.AddedAt,
                CreatedOn = DateTime.UtcNow,
            };

            await _dbContext.CartItems.AddAsync(CartItem);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> UpdateCartItemAsync(int id, AddOrUpdateCartItemDto dto)
    {
        try
        {
            CartItem getCartItem = await _dbContext.CartItems.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (getCartItem != null)
                return false;

            getCartItem.ProductId = dto.ProductId;
            getCartItem.Quantity = dto.Quantity;
            getCartItem.UpdatedOn = DateTime.UtcNow;

            _dbContext.CartItems.Update(getCartItem);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteCartItemAsync(int id, AddOrUpdateCartItemDto dto)
    {
        try
        {
            CartItem getCartItem = await _dbContext.CartItems.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (getCartItem != null)
                return false;

            getCartItem.IsDeleted = true;
            getCartItem.DeletedOn = DateTime.UtcNow;

            _dbContext.CartItems.Update(getCartItem);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
