namespace JAS.ECommerce.Domain.Enums;

public enum NotificationType
{
    OrderConfirmation = 0,
    OrderShipped = 1,
    OrderDelivered = 2,
    OrderCancelled = 3,
    Promotion = 4,
    FlashSale = 5,
    LowStock = 6,
    ReturnApproved = 7,
    ReturnRejected = 8,
    PaymentReceived = 9
}
