namespace JAS.ECommerce.Application.DTOs.Cart;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public IEnumerable<CartItemDto> Items { get; set; } = Enumerable.Empty<CartItemDto>();
    public int TotalItems => Items.Sum(i => i.Quantity);
    public decimal SubTotal => Items.Sum(i => i.Quantity * i.UnitPrice);
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int? ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}
