using JAS.Shared;
using JAS.Shared.Dto;
using JAS.Shared.Dto.Product;
using JAS.Shared.Dtos.Product;
using JASData.Models;
using Microsoft.AspNetCore.Mvc;

namespace JASApi.Controllers.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : Controller
{
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<ProductsListDto>>> GetAllProducts()
    {
        var products = await productService.GetAllProductsAsync();

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateProduct(ProductCreateOrUpdateDto productDto)
    {
        Product product = new Product()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Image = productDto.Image,
            Quantity = productDto.Quantity,
            IsFeatured = productDto.IsFeatured

        };

        bool result = await productService.AddProduct(product);



        return Ok(new BaseResponseModel { Success = result, Data = null, ErrorMessage = "tst" });
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<ProductDetailsDto>> GetProductByIdAsync(long id)
    {
        Product product = await productService.GetProductByIdAsync(id);

        return Ok(product);
    }

    [HttpPatch("Update/{id}")]
    public async Task<ActionResult<bool>> UpdateProductAsync(long id, Product updateProduct)
    {

        Product product = await productService.GetProductByIdAsync(id) ?? new Product();

        product.Name = updateProduct.Name;

        bool update = await productService.UpdateProduct(id, product);

        return update;
    }

    [HttpPatch("Delete/{id}")]
    public async Task<ActionResult<bool>> DeleteProductAsync(long id)
    {

        Product product = await productService.GetProductByIdAsync(id) ?? new Product();

        product.IsDeleted = true;
        product.DeletedOn = DateTime.UtcNow;

        bool update = await productService.UpdateProduct(id, product);

        return update;
    }

}
