namespace JAS.Shared.Dto.Inventory;

public class AddOrUpdateInventoryDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
    public long ProductId { get; set; }
}
