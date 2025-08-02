using JAS.Shared.Dto.Order;
using JAS.Shared.Dto;
using JAS.Shared.Dto.CartItem;
using JAS.Shared.Dto.Inventory;

namespace JAS.Shared.Dtos.Product;

public class ProductDetailsDto : BaseDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public int Quantity { get; set; }
    public bool IsFeatured { get; set; }
    public string SKU { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; }
    public ICollection<InventoryDto> InventoryItems { get; set; }
}
