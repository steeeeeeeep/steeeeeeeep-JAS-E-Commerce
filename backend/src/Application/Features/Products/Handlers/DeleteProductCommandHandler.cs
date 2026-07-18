using JAS.ECommerce.Application.Features.Products.Commands;
using JAS.ECommerce.Domain.Interfaces;
using MediatR;

namespace JAS.ECommerce.Application.Features.Products.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Product with ID {request.Id} not found");

        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
