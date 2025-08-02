using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JASInventories", Schema = "Inventories")]
public class Inventory : BaseEntity
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }

    public long ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
}
