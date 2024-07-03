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

    public async Task<Product> GetProductByIdAsync(long id)
    {
        return await _db.Set<Product>().FindAsync(id);
    }

    public async Task<Product> AddProduct(Product product)
    {
        var addProduct = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Quantity = product.Quantity,
            Image = product.Image,
            Featured = product.Featured,
            CreatedOn = product.CreatedOn,
            UpdatedOn = product.UpdatedOn,
        };

        _db.Set<Product>().Add(addProduct);
        await _db.SaveChangesAsync();
        return addProduct;
    }

    public async Task<Product> DeleteProduct(long id)
    {
        var toDelete = await _db.Products.FindAsync(id);

        if(toDelete != null)
        _db.Products.Remove(toDelete);
        await _db.SaveChangesAsync();

        return toDelete!;
    }

    public async Task<Product> UpdateProduct(long id, Product product)
    {
        _db.Entry(product).State = EntityState.Modified;
        
        try
        {
            await _db.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if (!ProductIsExisting(id))
            {
                return product;
            }
            else
            {
                throw;
            }
        }
        
        return product;
    }

    private bool ProductIsExisting(long productId) => (_db.Products?.Any(x => x.Id == productId)).GetValueOrDefault();
}
