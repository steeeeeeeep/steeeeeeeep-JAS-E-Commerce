using AutoMapper;
using JAS.ECommerce.Application.DTOs.Product;
using JAS.ECommerce.Application.Features.Products.Commands;
using JAS.ECommerce.Application.Validators.Product;
using JAS.ECommerce.Domain.Interfaces;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CreateProductValidator _validator;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, CreateProductValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate input
        var validationResult = await _validator.ValidateAsync(request.Product, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new FluentValidation.ValidationException(validationResult.Errors);
        }

        // Get product
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        // Update properties
        _mapper.Map(request.Product, product);
        product.UpdatedAt = DateTime.UtcNow;

        // Update in repository
        _unitOfWork.ProductRepository.Update(product);

        // Save changes
        await _unitOfWork.SaveChangesAsync();

        // Return mapped DTO
        return _mapper.Map<ProductDto>(product);
    }
}
