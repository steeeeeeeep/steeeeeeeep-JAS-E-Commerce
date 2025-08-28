using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static JAS.Shared.Enums.OrderEnum;

namespace JASData.Models;

[Table("JASOrders", Schema = "Products")]
public class Order : BaseEntity
{
    [Key, Required]
    public int Id { get; set; }
    
    public string ApplicationUserId { get; set; }
    [ForeignKey(nameof(ApplicationUserId))]
    public virtual ApplicationUser ApplicationUser { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

    public ProductPurchaseStatus Status { get; set; }
}
