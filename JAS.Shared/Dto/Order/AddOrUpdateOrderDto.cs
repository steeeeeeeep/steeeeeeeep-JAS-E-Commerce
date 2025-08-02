using static JAS.Shared.Enums.OrderEnum;

namespace JAS.Shared.Dto.Order; 

public class AddOrUpdateOrderDto : BaseDto
{
    public int Id { get; set; }
    public int ? ApplicationUserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public ProductPurchaseStatus Status { get; set; }
}
