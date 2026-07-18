using JAS.ECommerce.Domain.Entities;
using JAS.ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace JAS.ECommerce.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceDbContext _context;
    private IDbContextTransaction? _transaction;

    private IRepository<User>? _userRepository;
    private IRepository<Address>? _addressRepository;
    private IRepository<Product>? _productRepository;
    private IRepository<Category>? _categoryRepository;
    private IRepository<Brand>? _brandRepository;
    private IRepository<ProductImage>? _productImageRepository;
    private IRepository<ProductVariant>? _productVariantRepository;
    private IRepository<Inventory>? _inventoryRepository;
    private IRepository<InventoryTransaction>? _inventoryTransactionRepository;
    private IRepository<ShoppingCart>? _shoppingCartRepository;
    private IRepository<CartItem>? _cartItemRepository;
    private IRepository<Order>? _orderRepository;
    private IRepository<OrderItem>? _orderItemRepository;
    private IRepository<Payment>? _paymentRepository;
    private IRepository<Coupon>? _couponRepository;
    private IRepository<CouponUsage>? _couponUsageRepository;
    private IRepository<Review>? _reviewRepository;
    private IRepository<Wishlist>? _wishlistRepository;
    private IRepository<Notification>? _notificationRepository;
    private IRepository<RefreshToken>? _refreshTokenRepository;
    private IRepository<AuditLog>? _auditLogRepository;

    public UnitOfWork(ECommerceDbContext context)
    {
        _context = context;
    }

    public IRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);
    public IRepository<Address> AddressRepository => _addressRepository ??= new GenericRepository<Address>(_context);
    public IRepository<Product> ProductRepository => _productRepository ??= new GenericRepository<Product>(_context);
    public IRepository<Category> CategoryRepository => _categoryRepository ??= new GenericRepository<Category>(_context);
    public IRepository<Brand> BrandRepository => _brandRepository ??= new GenericRepository<Brand>(_context);
    public IRepository<ProductImage> ProductImageRepository => _productImageRepository ??= new GenericRepository<ProductImage>(_context);
    public IRepository<ProductVariant> ProductVariantRepository => _productVariantRepository ??= new GenericRepository<ProductVariant>(_context);
    public IRepository<Inventory> InventoryRepository => _inventoryRepository ??= new GenericRepository<Inventory>(_context);
    public IRepository<InventoryTransaction> InventoryTransactionRepository => _inventoryTransactionRepository ??= new GenericRepository<InventoryTransaction>(_context);
    public IRepository<ShoppingCart> ShoppingCartRepository => _shoppingCartRepository ??= new GenericRepository<ShoppingCart>(_context);
    public IRepository<CartItem> CartItemRepository => _cartItemRepository ??= new GenericRepository<CartItem>(_context);
    public IRepository<Order> OrderRepository => _orderRepository ??= new GenericRepository<Order>(_context);
    public IRepository<OrderItem> OrderItemRepository => _orderItemRepository ??= new GenericRepository<OrderItem>(_context);
    public IRepository<Payment> PaymentRepository => _paymentRepository ??= new GenericRepository<Payment>(_context);
    public IRepository<Coupon> CouponRepository => _couponRepository ??= new GenericRepository<Coupon>(_context);
    public IRepository<CouponUsage> CouponUsageRepository => _couponUsageRepository ??= new GenericRepository<CouponUsage>(_context);
    public IRepository<Review> ReviewRepository => _reviewRepository ??= new GenericRepository<Review>(_context);
    public IRepository<Wishlist> WishlistRepository => _wishlistRepository ??= new GenericRepository<Wishlist>(_context);
    public IRepository<Notification> NotificationRepository => _notificationRepository ??= new GenericRepository<Notification>(_context);
    public IRepository<RefreshToken> RefreshTokenRepository => _refreshTokenRepository ??= new GenericRepository<RefreshToken>(_context);
    public IRepository<AuditLog> AuditLogRepository => _auditLogRepository ??= new GenericRepository<AuditLog>(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction != null;
    }

    public async Task<bool> CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
            return true;
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync()!;
            return true;
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _context?.Dispose();
        _transaction?.Dispose();
    }
}
