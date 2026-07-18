namespace JAS.ECommerce.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int HelpfulCount { get; set; }
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Product Product { get; set; } = null!;
    public User User { get; set; } = null!;
}
