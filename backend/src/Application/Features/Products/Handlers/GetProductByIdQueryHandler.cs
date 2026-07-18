using AutoMapper;
using JAS.ECommerce.Application.DTOs.Product;
using JAS.ECommerce.Application.Features.Products.Queries;
using JAS.ECommerce.Domain.Interfaces;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        return product != null ? _mapper.Map<ProductDto>(product) : null;
    }
}
