namespace JAS.ECommerce.Application.DTOs.Product;

public class CreateProductDto
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public int CategoryId { get; set; }
    public int? BrandId { get; set; }
}
