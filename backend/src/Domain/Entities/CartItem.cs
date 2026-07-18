namespace JAS.ECommerce.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ShoppingCart ShoppingCart { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public ProductVariant? ProductVariant { get; set; }
}
