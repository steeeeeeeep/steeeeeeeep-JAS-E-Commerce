using JAS.Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace JAS.Shared.Dto.ProductCategory;

public class ProductCategoryCreateOrUpdateDto : BaseDto
{
    public int ProductCategoryId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    public string? Description { get; set; }
    public int Quantity { get; set; }
    public bool IsFeatured { get; set; }
}
