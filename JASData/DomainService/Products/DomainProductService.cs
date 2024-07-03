using JASData.Models;

namespace JASData.DomainService.Products;

public interface IDomainProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId);
    Task<Product> GetProductByIdAsync(long id);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(long id, Product product);
    Task<Product> DeleteProduct(long id);
}

public class DomainProductService : IDomainProductService
{
    private readonly IProductRepository _repository;

    public DomainProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllProductsAsync();
    }

    public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> GetProductByIdAsync(long id)
    {
        return await _repository.GetProductByIdAsync(id);
    }
    public async Task<Product> AddProduct(Product product)
    {
        return await _repository.AddProduct(product);
    }

    public async Task<Product> DeleteProduct(long id)
    {
        return await _repository.DeleteProduct(id);
    }

    public async Task<Product> UpdateProduct(long id, Product product)
    {
        return await _repository.UpdateProduct(id, product);
    }
}
