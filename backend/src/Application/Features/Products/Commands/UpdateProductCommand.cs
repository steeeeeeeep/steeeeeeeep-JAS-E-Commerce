using JAS.ECommerce.Application.DTOs.Product;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Commands;

public record UpdateProductCommand(int Id, CreateProductDto Product) : IRequest<ProductDto>;
