using JAS.ECommerce.Domain.Enums;

namespace JAS.ECommerce.Domain.Entities;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Data { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReadAt { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
}
