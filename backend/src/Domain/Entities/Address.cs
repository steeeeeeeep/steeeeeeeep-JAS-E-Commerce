namespace JAS.ECommerce.Domain.Entities;

public class Address
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsShipping { get; set; } = true;
    public bool IsBilling { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
