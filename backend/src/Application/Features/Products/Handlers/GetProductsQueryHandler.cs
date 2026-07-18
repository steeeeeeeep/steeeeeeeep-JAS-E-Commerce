using AutoMapper;
using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Product;
using JAS.ECommerce.Application.Features.Products.Queries;
using JAS.ECommerce.Domain.Interfaces;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Handlers;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedResponse<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        var totalCount = products.Count();
        var paginatedProducts = products
            .Skip((request.Pagination.Page - 1) * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize)
            .ToList();

        return new PaginatedResponse<ProductDto>
        {
            Items = _mapper.Map<IEnumerable<ProductDto>>(paginatedProducts),
            TotalCount = totalCount,
            Page = request.Pagination.Page,
            PageSize = request.Pagination.PageSize
        };
    }
}
