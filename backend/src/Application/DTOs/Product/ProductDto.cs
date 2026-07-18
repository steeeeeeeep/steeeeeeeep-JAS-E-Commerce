namespace JAS.ECommerce.Application.DTOs.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int CategoryId { get; set; }
    public int? BrandId { get; set; }
    public int Rating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
