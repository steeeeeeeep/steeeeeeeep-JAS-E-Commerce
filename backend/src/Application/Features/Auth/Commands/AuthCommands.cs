using JAS.ECommerce.Application.DTOs.Auth;
using MediatR;

namespace JAS.ECommerce.Application.Features.Auth.Commands;

public record RegisterCommand(RegisterDto RegisterData) : IRequest<(bool Success, string? Error)>;
public record LoginCommand(LoginDto LoginData) : IRequest<AuthResponseDto>;
public record RefreshTokenCommand(RefreshTokenDto RefreshTokenData) : IRequest<AuthResponseDto>;
public record LogoutCommand(string Token) : IRequest<bool>;
