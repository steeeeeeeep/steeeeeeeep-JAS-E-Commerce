using JAS.ECommerce.Application.DTOs.Cart;

namespace JAS.ECommerce.Application.Interfaces;

public interface ICartService
{
    Task<CartDto?> GetCartByUserIdAsync(int userId);
    Task<CartDto> AddToCartAsync(int userId, int productId, int quantity, int? variantId = null);
    Task<CartDto> RemoveFromCartAsync(int userId, int cartItemId);
    Task<CartDto> UpdateCartItemQuantityAsync(int userId, int cartItemId, int quantity);
    Task<CartDto> ClearCartAsync(int userId);
}
