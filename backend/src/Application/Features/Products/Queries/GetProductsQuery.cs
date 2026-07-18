using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Product;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Queries;

public record GetProductsQuery(PaginationDto Pagination) : IRequest<PaginatedResponse<ProductDto>>;
