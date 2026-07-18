namespace JAS.ECommerce.Application.DTOs.Inventory;

public class InventoryDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int AvailableQuantity => Quantity - ReservedQuantity;
    public int LowStockThreshold { get; set; }
    public bool IsLowStock => AvailableQuantity <= LowStockThreshold;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class InventoryTransactionDto
{
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public int Type { get; set; }
    public int Quantity { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
