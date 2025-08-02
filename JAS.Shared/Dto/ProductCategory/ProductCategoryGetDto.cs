using System.ComponentModel.DataAnnotations;

namespace JAS.Shared.Dto.ProductCategory;

public class ProductCategoryGetDto
{
    public int ProductCategoryId { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }
    public int Quantity { get; set; }
    public bool IsFeatured { get; set; }
}
