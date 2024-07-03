using JASApi.Data;
using JASData.DomainService.Products;
using JASData.Models;

namespace JASApi.Controllers.Products;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync();
    public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId);
    public Task<Product> GetProductByIdAsync(long id);
    public Task<Product> UpdateProduct(long id, Product product);
    public Task<Product> AddProduct(Product product);
    public Task<Product> DeleteProduct(long id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly AppDbContext _db;

    public ProductService(IProductRepository productRepository, AppDbContext dbContext)
    {
        _productRepository = productRepository;
        _db = dbContext;
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return _productRepository.GetAllProductsAsync();
    }

    public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateProduct(long id, Product product)
    {
        throw new NotImplementedException();
    }
    public Task<Product> AddProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product> DeleteProduct(long id)
    {
        throw new NotImplementedException();
    }
}
