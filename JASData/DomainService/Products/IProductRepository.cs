using JASData.Models;

namespace JASData.DomainService.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetAllProductsByCategoryAsync();
    Task<Product> GetProductByIdAsync(long id);
    Task<bool> AddProduct(Product product);
    Task<bool> UpdateProduct(long id, Product product);
    Task<bool> DeleteProduct(long id);
}
