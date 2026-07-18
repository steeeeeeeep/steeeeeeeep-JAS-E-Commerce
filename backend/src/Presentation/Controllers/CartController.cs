using JAS.ECommerce.Application.DTOs.Cart;
using JAS.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JAS.ECommerce.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartController> _logger;

    public CartController(ICartService cartService, ILogger<CartController> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        try
        {
            var userId = GetUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
                return NotFound(new { success = false, message = "Cart not found" });

            return Ok(new { success = true, data = cart });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cart");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addToCartDto)
    {
        try
        {
            var userId = GetUserId();
            var cart = await _cartService.AddToCartAsync(userId, addToCartDto.ProductId, addToCartDto.Quantity, addToCartDto.VariantId);
            return Ok(new { success = true, message = "Item added to cart", data = cart });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding to cart");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpDelete("remove-item/{cartItemId}")]
    public async Task<IActionResult> RemoveFromCart(int cartItemId)
    {
        try
        {
            var userId = GetUserId();
            var cart = await _cartService.RemoveFromCartAsync(userId, cartItemId);
            return Ok(new { success = true, message = "Item removed from cart", data = cart });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing from cart");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpPut("update-quantity/{cartItemId}")]
    public async Task<IActionResult> UpdateQuantity(int cartItemId, [FromBody] UpdateQuantityDto updateQuantityDto)
    {
        try
        {
            var userId = GetUserId();
            var cart = await _cartService.UpdateCartItemQuantityAsync(userId, cartItemId, updateQuantityDto.Quantity);
            return Ok(new { success = true, message = "Quantity updated", data = cart });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating quantity");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart()
    {
        try
        {
            var userId = GetUserId();
            var cart = await _cartService.ClearCartAsync(userId);
            return Ok(new { success = true, message = "Cart cleared", data = cart });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing cart");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim ?? throw new UnauthorizedAccessException());
    }
}

public class AddToCartDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int? VariantId { get; set; }
}

public class UpdateQuantityDto
{
    public int Quantity { get; set; }
}
