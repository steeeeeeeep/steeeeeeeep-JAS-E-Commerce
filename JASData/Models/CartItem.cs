using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JasCartItems", Schema = "CartItems")]
public class CartItem : BaseEntity
{
    public int Id { get; set; }
    public long ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedOn { get; set; }
}
