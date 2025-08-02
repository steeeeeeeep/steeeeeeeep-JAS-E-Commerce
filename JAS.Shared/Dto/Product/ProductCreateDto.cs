using JAS.Shared.Dto;
using JAS.Shared.Dto.CartItem;
using JAS.Shared.Dto.Inventory;
using JAS.Shared.Dto.Order;
using System.ComponentModel.DataAnnotations;

namespace JAS.Shared.Dtos.Product;

public class ProductCreateDto : BaseDto
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.1, 99999.99, ErrorMessage = "Price must be between 0.1 and 99999.99.")]
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
