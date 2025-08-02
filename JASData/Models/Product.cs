using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JASProducts", Schema = "Products")]
public class Product : BaseEntity
{
    [Key]
    [Required]
    public long Id { get; set; }

    public int ? ProductCategoryId { get; set; }
    [ForeignKey(nameof(ProductCategoryId))]
    public virtual ProductCategory ProductCategory { get; set;}

    public int ? BrandId { get; set; }
    [ForeignKey(nameof(BrandId))]
    public virtual Brand Brand { get; set;}

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.1, 99999.99, ErrorMessage = "Price must be between 0.1 and 99999.99.")]
    public decimal Price { get; set; }
    public string Image { get; set; }
    public int Quantity { get; set; }
    public bool IsFeatured { get; set; }
    public string SKU { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<Inventory> InventoryItems { get; set; }
}