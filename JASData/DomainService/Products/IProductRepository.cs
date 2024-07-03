using JASData.Models;

namespace JASData.DomainService.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetAllProductsByCategoryAsync();
    Task<Product> GetProductByIdAsync(long id);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(long id, Product product);
    Task<Product> DeleteProduct(long id);
}
