using JAS.ECommerce.Domain.Entities;

namespace JAS.ECommerce.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Address> AddressRepository { get; }
    IRepository<Product> ProductRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    IRepository<Brand> BrandRepository { get; }
    IRepository<ProductImage> ProductImageRepository { get; }
    IRepository<ProductVariant> ProductVariantRepository { get; }
    IRepository<Inventory> InventoryRepository { get; }
    IRepository<InventoryTransaction> InventoryTransactionRepository { get; }
    IRepository<ShoppingCart> ShoppingCartRepository { get; }
    IRepository<CartItem> CartItemRepository { get; }
    IRepository<Order> OrderRepository { get; }
    IRepository<OrderItem> OrderItemRepository { get; }
    IRepository<Payment> PaymentRepository { get; }
    IRepository<Coupon> CouponRepository { get; }
    IRepository<CouponUsage> CouponUsageRepository { get; }
    IRepository<Review> ReviewRepository { get; }
    IRepository<Wishlist> WishlistRepository { get; }
    IRepository<Notification> NotificationRepository { get; }
    IRepository<RefreshToken> RefreshTokenRepository { get; }
    IRepository<AuditLog> AuditLogRepository { get; }

    Task<int> SaveChangesAsync();
    Task<bool> BeginTransactionAsync();
    Task<bool> CommitTransactionAsync();
    Task<bool> RollbackTransactionAsync();
}
