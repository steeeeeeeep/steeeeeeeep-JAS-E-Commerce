using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JASData.Models;

[Table("JASProductCategory", Schema = "Products")]
public class ProductCategory : BaseEntity
{
    [Required,Key]
    public int ProductCategoryId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    public string Description { get; set; }
    public int Quantity { get; set; }
    public bool Featured { get; set; }
    public int? ParentCategoryId { get; set; }
    [ForeignKey(nameof(ParentCategoryId))]
    public ProductCategory ParentCategory { get; set; }
    public ICollection<ProductCategory> SubCategories { get; set; }
    public ICollection<Product> Products { get; set; }
}
