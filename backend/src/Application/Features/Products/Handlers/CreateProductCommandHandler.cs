using AutoMapper;
using JAS.ECommerce.Application.DTOs.Product;
using JAS.ECommerce.Application.Features.Products.Commands;
using JAS.ECommerce.Application.Validators.Product;
using JAS.ECommerce.Domain.Entities;
using JAS.ECommerce.Domain.Interfaces;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateProductValidator _validator;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, CreateProductValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate input
        var validationResult = await _validator.ValidateAsync(request.Product, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        // Map DTO to entity
        var product = _mapper.Map<Product>(request.Product);

        // Add to repository
        await _unitOfWork.ProductRepository.AddAsync(product);

        // Save changes
        await _unitOfWork.SaveChangesAsync();

        // Return mapped DTO
        return _mapper.Map<ProductDto>(product);
    }
}
