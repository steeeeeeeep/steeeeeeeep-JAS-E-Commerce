using Duende.IdentityServer.EntityFramework.Options;
using JASData.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JASApp.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options,
    IOptions<OperationalStoreOptions> operationalStoreOptions) : ApiAuthorizationDbContext<ApplicationUser>(options, operationalStoreOptions)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductCategory>()
        .HasOne(pc => pc.ParentCategory)
        .WithMany(pc => pc.SubCategories)
        .HasForeignKey(pc => pc.ParentCategoryId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete loops

        modelBuilder.Entity<ProductCategory>()
            .HasMany(pc => pc.Products)
            .WithOne(p => p.ProductCategory) // Assuming Product has a Category navigation
            .HasForeignKey(p => p.ProductCategoryId);
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Purchase> Purchases { get; set; } = default!;
    public DbSet<Brand> Brands { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
    public DbSet<ProductImage> ProductImages { get; set; } = default!;
    public DbSet<CartItem> CartItems { get; set; } = default!;
    public DbSet<Inventory> Inventories { get; set; } = default!;
}
