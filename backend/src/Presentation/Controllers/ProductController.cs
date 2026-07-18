using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Product;
using JAS.ECommerce.Application.Features.Products.Commands;
using JAS.ECommerce.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JAS.ECommerce.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetProductById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
            return NotFound(ApiResponse<ProductDto>.FailureResponse("Product not found"));

        return Ok(ApiResponse<ProductDto>.SuccessResponse(product));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PaginatedResponse<ProductDto>>>> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var pagination = new PaginationDto { Page = page, PageSize = pageSize };
        var products = await _mediator.Send(new GetProductsQuery(pagination));
        return Ok(ApiResponse<PaginatedResponse<ProductDto>>.SuccessResponse(products));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct(CreateProductDto createProductDto)
    {
        var product = await _mediator.Send(new CreateProductCommand(createProductDto));
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id },
            ApiResponse<ProductDto>.SuccessResponse(product, "Product created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(int id, CreateProductDto updateProductDto)
    {
        var product = await _mediator.Send(new UpdateProductCommand(id, updateProductDto));
        return Ok(ApiResponse<ProductDto>.SuccessResponse(product, "Product updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(int id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return Ok(ApiResponse<bool>.SuccessResponse(true, "Product deleted successfully"));
    }
}
