namespace JAS.Shared.Dto.Inventory;

public class InventoryDto : BaseDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }

    public long ProductId { get; set; }
}
