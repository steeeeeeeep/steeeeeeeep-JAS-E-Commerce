using System.ComponentModel.DataAnnotations.Schema;

namespace JASData.Models;

[Table("JasPayments", Schema = "Payments")]
public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
    public DateTime PaymentDate { get; set; }
    public string PaymentStatus { get; set; } // e.g., Paid, Pending, Failed
}
