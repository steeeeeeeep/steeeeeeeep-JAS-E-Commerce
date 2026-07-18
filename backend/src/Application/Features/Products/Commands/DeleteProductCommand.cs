using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest<bool>;
