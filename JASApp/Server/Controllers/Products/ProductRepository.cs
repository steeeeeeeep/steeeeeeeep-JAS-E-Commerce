using JASApi.Data;
using JASData.DomainService.Products;
using JASData.Models;
using Microsoft.EntityFrameworkCore;

namespace JASApi.Controllers.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext appDbContext)
    {
        _db = appDbContext;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _db.Products.ToListAsync();
    }

    public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetProductByIdAsync(long id)
    {
        return await _db.Set<Product>().FindAsync(id);
    }

    public async Task<bool> AddProduct(Product product)
    {
        var addProduct = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Quantity = product.Quantity,
            Image = product.Image,
            IsFeatured = product.IsFeatured,
            CreatedOn = product.CreatedOn,
            UpdatedOn = product.UpdatedOn,
        };

        _db.Set<Product>().Add(addProduct);
        int result = await _db.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteProduct(long id)
    {
        var toDelete = await _db.Products.FindAsync(id);

        if(toDelete != null)
        _db.Products.Remove(toDelete);
        int result = await _db.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> UpdateProduct(long id, Product product)
    {
        var existingProduct = await _db.Products.FindAsync(id);

        if (existingProduct == null)
            return false;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;


        try
        {
            int result = await _db.SaveChangesAsync();
            return result > 0;
        }
        catch(DbUpdateConcurrencyException)
        {
            if (!ProductIsExisting(id))
            {
                return false;
            }
            else
            {
                throw;
            }
        }
    }

    private bool ProductIsExisting(long productId) => (_db.Products?.Any(x => x.Id == productId)).GetValueOrDefault();
}
