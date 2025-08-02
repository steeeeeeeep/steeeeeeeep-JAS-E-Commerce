using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JASBrand", Schema = "Reference")]
public class Brand : BaseEntity
{
    [Key, Required]
    public int BrandId { get; set; }

    [Required]
    public string BrandName { get; set; }

    public string Description { get; set; }
    public bool IsFeatured { set; get; }

}
