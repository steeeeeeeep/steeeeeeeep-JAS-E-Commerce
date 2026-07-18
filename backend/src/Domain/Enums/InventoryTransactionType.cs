namespace JAS.ECommerce.Domain.Enums;

public enum InventoryTransactionType
{
    Inbound = 0,
    Outbound = 1,
    Adjustment = 2,
    Reservation = 3,
    ReservationCancellation = 4,
    Return = 5
}
