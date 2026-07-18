using JAS.ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JAS.ECommerce.Infrastructure.Persistence;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<CouponUsage> CouponUsages { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Slug).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Slug).IsUnique();
        });

        // Product configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Sku).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.DiscountPrice).HasColumnType("decimal(18,2)");
            entity.HasIndex(e => e.Sku).IsUnique();
            entity.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
            entity.HasOne(e => e.Brand).WithMany(e => e.Products).HasForeignKey(e => e.BrandId).IsRequired(false);
        });

        // Order configuration
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18,2)");
            entity.HasIndex(e => e.OrderNumber).IsUnique();
            entity.HasOne(e => e.User).WithMany(e => e.Orders).HasForeignKey(e => e.UserId);
        });
    }
}
