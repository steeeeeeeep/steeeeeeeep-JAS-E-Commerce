using JAS.ECommerce.Application.DTOs.Cart;
using JAS.ECommerce.Application.Interfaces;
using JAS.ECommerce.Domain.Entities;
using JAS.ECommerce.Domain.Interfaces;

namespace JAS.ECommerce.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CartService> _logger;

    public CartService(IUnitOfWork unitOfWork, ILogger<CartService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CartDto?> GetCartByUserIdAsync(int userId)
    {
        try
        {
            var carts = await _unitOfWork.ShoppingCartRepository.GetAllAsync();
            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
                return null;

            var cartItems = await _unitOfWork.CartItemRepository.GetAllAsync();
            var items = cartItems.Where(ci => ci.ShoppingCartId == cart.Id).ToList();

            return MapToDto(cart, items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cart");
            throw;
        }
    }

    public async Task<CartDto> AddToCartAsync(int userId, int productId, int quantity, int? variantId = null)
    {
        try
        {
            var carts = await _unitOfWork.ShoppingCartRepository.GetAllAsync();
            var cart = carts.FirstOrDefault(c => c.UserId == userId)
                ?? throw new KeyNotFoundException("Cart not found");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId)
                ?? throw new KeyNotFoundException("Product not found");

            var cartItems = await _unitOfWork.CartItemRepository.GetAllAsync();
            var existingItem = cartItems.FirstOrDefault(ci =>
                ci.ShoppingCartId == cart.Id &&
                ci.ProductId == productId &&
                ci.ProductVariantId == variantId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.CartItemRepository.Update(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    ShoppingCartId = cart.Id,
                    ProductId = productId,
                    ProductVariantId = variantId,
                    Quantity = quantity,
                    UnitPrice = product.DiscountPrice ?? product.Price,
                    AddedAt = DateTime.UtcNow
                };
                await _unitOfWork.CartItemRepository.AddAsync(newItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Item added to cart for user {userId}");
            return await GetCartByUserIdAsync(userId) ?? throw new InvalidOperationException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding to cart");
            throw;
        }
    }

    public async Task<CartDto> RemoveFromCartAsync(int userId, int cartItemId)
    {
        try
        {
            var cartItem = await _unitOfWork.CartItemRepository.GetByIdAsync(cartItemId)
                ?? throw new KeyNotFoundException("Cart item not found");

            var carts = await _unitOfWork.ShoppingCartRepository.GetAllAsync();
            var cart = carts.FirstOrDefault(c => c.UserId == userId && c.Id == cartItem.ShoppingCartId)
                ?? throw new UnauthorizedAccessException("Not authorized to remove this item");

            _unitOfWork.CartItemRepository.Delete(cartItem);
            cart.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Item removed from cart for user {userId}");
            return await GetCartByUserIdAsync(userId) ?? throw new InvalidOperationException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing from cart");
            throw;
        }
    }

    public async Task<CartDto> UpdateCartItemQuantityAsync(int userId, int cartItemId, int quantity)
    {
        try
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            var cartItem = await _unitOfWork.CartItemRepository.GetByIdAsync(cartItemId)
                ?? throw new KeyNotFoundException("Cart item not found");

            var carts = await _unitOfWork.ShoppingCartRepository.GetAllAsync();
            var cart = carts.FirstOrDefault(c => c.UserId == userId && c.Id == cartItem.ShoppingCartId)
                ?? throw new UnauthorizedAccessException("Not authorized to update this item");

            cartItem.Quantity = quantity;
            cartItem.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.CartItemRepository.Update(cartItem);

            cart.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Cart item quantity updated for user {userId}");
            return await GetCartByUserIdAsync(userId) ?? throw new InvalidOperationException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cart item quantity");
            throw;
        }
    }

    public async Task<CartDto> ClearCartAsync(int userId)
    {
        try
        {
            var carts = await _unitOfWork.ShoppingCartRepository.GetAllAsync();
            var cart = carts.FirstOrDefault(c => c.UserId == userId)
                ?? throw new KeyNotFoundException("Cart not found");

            var cartItems = await _unitOfWork.CartItemRepository.GetAllAsync();
            var itemsToDelete = cartItems.Where(ci => ci.ShoppingCartId == cart.Id).ToList();

            foreach (var item in itemsToDelete)
            {
                _unitOfWork.CartItemRepository.Delete(item);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.ShoppingCartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Cart cleared for user {userId}");
            return await GetCartByUserIdAsync(userId) ?? throw new InvalidOperationException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing cart");
            throw;
        }
    }

    private static CartDto MapToDto(ShoppingCart cart, List<CartItem> items)
    {
        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = items.Select(i => new CartItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.Product?.Name ?? string.Empty,
                ProductVariantId = i.ProductVariantId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }),
            CreatedAt = cart.CreatedAt,
            UpdatedAt = cart.UpdatedAt
        };
    }
}
