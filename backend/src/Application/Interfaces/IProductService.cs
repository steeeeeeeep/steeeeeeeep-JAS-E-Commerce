using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Product;

namespace JAS.ECommerce.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<PaginatedResponse<ProductDto>> GetProductsAsync(PaginationDto pagination);
    Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductDto> UpdateProductAsync(int id, CreateProductDto updateProductDto);
    Task DeleteProductAsync(int id);
}
