using JAS.ECommerce.Domain.Enums;

namespace JAS.ECommerce.Domain.Entities;

public class InventoryTransaction
{
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public InventoryTransactionType Type { get; set; }
    public int Quantity { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Inventory Inventory { get; set; } = null!;
}
