namespace JAS.ECommerce.Domain.Entities;

public class CouponUsage
{
    public int Id { get; set; }
    public int CouponId { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Coupon Coupon { get; set; } = null!;
}
