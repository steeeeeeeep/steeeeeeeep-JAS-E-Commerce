using JAS.Shared.Dto.ProductCategory;
using JASData.Models;

namespace JASData.DomainService.ProductCategories;

public class DomainProductCategroyService : IDomainProductCategroyService
{
    public Task<ProductCategoryGetDto> AddProduct(ProductCategoryGetDto model)
    {
        throw new NotImplementedException();
    }

    public Task<ProductCategoryGetDto> DeleteASync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductCategoryGetDto> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductCategoryGetDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductCategoryGetDto> UpdateProduct(int id, ProductCategoryGetDto model)
    {
        throw new NotImplementedException();
    }
}

public interface IDomainProductCategroyService : IJASService<ProductCategoryGetDto>
{
    Task<ProductCategoryGetDto> AddProduct(ProductCategoryGetDto model);
    Task<ProductCategoryGetDto> UpdateProduct(int id, ProductCategoryGetDto model);
}
