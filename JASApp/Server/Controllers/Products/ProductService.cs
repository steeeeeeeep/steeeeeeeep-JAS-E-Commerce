using JASApp.Api.Data;
using JASData.DomainService.Products;
using JASData.Models;

namespace JASApi.Controllers.Products;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync();
    public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId);
    public Task<Product> GetProductByIdAsync(long id);
    public Task<bool> UpdateProduct(long id, Product product);
    public Task<bool> AddProduct(Product product);
    public Task<bool> DeleteProduct(long id);
}

public class ProductService : IProductService
{
    private readonly IDomainProductService _productService;
    private readonly AppDbContext _db;

    public ProductService(IDomainProductService productService, AppDbContext dbContext)
    {
        _productService = productService;
        _db = dbContext;
    }

    //public async Task<IEnumerable<Product>> GetAllProductsAsync()
    //{
    //    var result = await ExecuteAsync();
    //}

    public async Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId)
    {
        return await _productService.GetAllProductsByCategoryAsync(categoryId);
    }

    public async Task<Product> GetProductByIdAsync(long id)
    {
        return await _productService.GetProductByIdAsync(id);
    }

    public async Task<bool> UpdateProduct(long id, Product product)
    {
        return await _productService.UpdateProduct(id, product);
    }
    public async Task<bool> AddProduct(Product product)
    {
        var result = await _productService.AddProduct(product);

        return result;
    }

    public async Task<bool> DeleteProduct(long id)
    {
        return await _productService.DeleteProduct(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var result = await _productService.GetAllProductsAsync();

        return result;
    }
}
