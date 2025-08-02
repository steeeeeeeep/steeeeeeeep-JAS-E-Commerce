using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JASProductImages", Schema = "Products")]
public class ProductImage : BaseEntity
{
    [Key]
    public long ProductImageId { get; set; }

    [Required]
    public long ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }

    public string Url { get; set; }
}
