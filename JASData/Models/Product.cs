using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JASProduct", Schema = "Reference")]
public class Product : BaseEntity
{
    [Key]
    [Required]
    public long Id { get; set; }

    [Required]
    public int ProductCategoryId { get; set; }
    [ForeignKey(nameof(ProductCategoryId))]
    public virtual ProductCategory ProductCategory { get; set;}

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
}
