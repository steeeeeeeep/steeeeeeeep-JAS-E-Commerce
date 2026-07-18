using JAS.ECommerce.Application.DTOs.Auth;
using JAS.ECommerce.Application.Features.Auth.Commands;
using JAS.ECommerce.Application.Interfaces;
using MediatR;

namespace JAS.ECommerce.Application.Features.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IAuthService _authService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(IAuthService authService, ILogger<LoginCommandHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (success, token, refreshToken, error) = await _authService.LoginAsync(
            request.LoginData.Email,
            request.LoginData.Password);

        if (!success)
        {
            throw new UnauthorizedAccessException(error ?? "Login failed");
        }

        return new AuthResponseDto
        {
            AccessToken = token!,
            RefreshToken = refreshToken!,
            ExpiresIn = DateTime.UtcNow.AddMinutes(15)
        };
    }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (bool Success, string? Error)>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<(bool Success, string? Error)> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(
            request.RegisterData.Email,
            request.RegisterData.FullName,
            request.RegisterData.PhoneNumber,
            request.RegisterData.Password);
    }
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var (success, token, error) = await _authService.RefreshTokenAsync(request.RefreshTokenData.RefreshToken);

        if (!success)
        {
            throw new UnauthorizedAccessException(error ?? "Token refresh failed");
        }

        return new AuthResponseDto
        {
            AccessToken = token!,
            RefreshToken = request.RefreshTokenData.RefreshToken,
            ExpiresIn = DateTime.UtcNow.AddMinutes(15)
        };
    }
}

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly IAuthService _authService;

    public LogoutCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RevokeTokenAsync(request.Token);
    }
}
