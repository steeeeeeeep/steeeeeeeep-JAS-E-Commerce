using System.ComponentModel.DataAnnotations.Schema;

namespace JAS.Shared.Dto.Payment;

public class PaymentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
    public DateTime PaymentDate { get; set; }
    public string PaymentStatus { get; set; } // e.g., Paid, Pending, Failed
}
