using FluentValidation;
using JAS.ECommerce.Application.DTOs.Product;

namespace JAS.ECommerce.Application.Validators.Product;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("SKU is required")
            .Length(1, 50).WithMessage("SKU must be between 1 and 50 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .Length(1, 255).WithMessage("Product name must be between 1 and 255 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .Length(1, 2000).WithMessage("Description must be between 1 and 2000 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.DiscountPrice)
            .LessThan(x => x.Price).WithMessage("Discount price must be less than regular price")
            .When(x => x.DiscountPrice.HasValue);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Valid category is required");
    }
}
