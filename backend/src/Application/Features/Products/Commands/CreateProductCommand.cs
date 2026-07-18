using JAS.ECommerce.Application.DTOs.Product;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Commands;

public record CreateProductCommand(CreateProductDto Product) : IRequest<ProductDto>;
