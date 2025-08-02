namespace JAS.Shared.Dto.CartItem;

public class CartItemDto : BaseDto
{
    public int Id { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedOn { get; set; }
}
