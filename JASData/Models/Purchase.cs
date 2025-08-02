using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JASPurchases", Schema = "Reference")]
public class Purchase : BaseEntity
{
    [Key]
    [Required]
    public long PurchaseId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
