using System.ComponentModel.DataAnnotations;

namespace JAS.Shared.Dtos.Product;

public class ProductCreateDto
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.1, 99999.99, ErrorMessage = "Price must be between 0.1 and 99999.99.")]
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public int Quantity { get; set; }
    public bool Featured { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
}
