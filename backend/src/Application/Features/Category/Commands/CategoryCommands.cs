using JAS.ECommerce.Application.DTOs.Auth;
using JAS.ECommerce.Application.Features.Auth.Commands;
using MediatR;

namespace JAS.ECommerce.Application.Features.Category.Commands;

public record CreateCategoryCommand(
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl,
    int? ParentCategoryId) : IRequest<int>;

public record UpdateCategoryCommand(
    int Id,
    string Name,
    string Slug,
    string? Description,
    string? ImageUrl) : IRequest<bool>;

public record DeleteCategoryCommand(int Id) : IRequest<bool>;
