using JAS.ECommerce.Application.DTOs.Product;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
