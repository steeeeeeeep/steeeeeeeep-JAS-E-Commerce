namespace JAS.ECommerce.Domain.Entities;

public class Inventory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int LowStockThreshold { get; set; } = 10;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Product Product { get; set; } = null!;
    public ProductVariant? ProductVariant { get; set; }
    public ICollection<InventoryTransaction> Transactions { get; set; } = new List<InventoryTransaction>();
}
