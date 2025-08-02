namespace JAS.Shared.Dto.Order;

public class OrderItemDto : BaseDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public long ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
