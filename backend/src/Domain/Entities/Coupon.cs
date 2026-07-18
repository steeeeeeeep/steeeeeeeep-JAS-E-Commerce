using JAS.ECommerce.Domain.Enums;

namespace JAS.ECommerce.Domain.Entities;

public class Coupon
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CouponType Type { get; set; }
    public decimal Value { get; set; }
    public decimal? MaxDiscount { get; set; }
    public decimal? MinPurchaseAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int CurrentUsageCount { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<CouponUsage> Usages { get; set; } = new List<CouponUsage>();
}
