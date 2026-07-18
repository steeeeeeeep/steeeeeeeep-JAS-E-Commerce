using BCrypt.Net;
using JAS.ECommerce.Application.DTOs.Auth;
using JAS.ECommerce.Application.Interfaces;
using JAS.ECommerce.Domain.Entities;
using JAS.ECommerce.Domain.Interfaces;

namespace JAS.ECommerce.Infrastructure.Services.Identity;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService,
        ILogger<AuthService> logger)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<(bool Success, string? Token, string? RefreshToken, string? Error)> LoginAsync(string email, string password)
    {
        try
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == email && u.IsActive);

            if (user == null)
            {
                _logger.LogWarning($"Login attempt failed for email: {email}");
                return (false, null, null, "Invalid email or password");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                _logger.LogWarning($"Password verification failed for user: {email}");
                return (false, null, null, "Invalid email or password");
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            // Generate tokens
            var accessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Email);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            // Save refresh token
            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"User logged in successfully: {email}");
            return (true, accessToken, refreshToken, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return (false, null, null, "An error occurred during login");
        }
    }

    public async Task<(bool Success, string? Error)> RegisterAsync(string email, string fullName, string phoneNumber, string password)
    {
        try
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            if (users.Any(u => u.Email == email))
            {
                return (false, "Email already registered");
            }

            var user = new User
            {
                Email = email,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                IsEmailConfirmed = false,
                IsPhoneConfirmed = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Create shopping cart for user
            var cart = new ShoppingCart
            {
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.ShoppingCartRepository.AddAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"User registered successfully: {email}");
            return (true, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            return (false, "An error occurred during registration");
        }
    }

    public async Task<(bool Success, string? Token, string? Error)> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var refreshTokens = await _unitOfWork.RefreshTokenRepository.GetAllAsync();
            var token = refreshTokens.FirstOrDefault(t => t.Token == refreshToken && !t.IsRevoked);

            if (token == null || token.ExpiryDate < DateTime.UtcNow)
            {
                return (false, null, "Invalid or expired refresh token");
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(token.UserId);
            if (user == null || !user.IsActive)
            {
                return (false, null, "User not found or inactive");
            }

            var newAccessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Email);
            return (true, newAccessToken, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refreshing token");
            return (false, null, "An error occurred while refreshing token");
        }
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        try
        {
            var refreshTokens = await _unitOfWork.RefreshTokenRepository.GetAllAsync();
            var refreshToken = refreshTokens.FirstOrDefault(t => t.Token == token);

            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                _unitOfWork.RefreshTokenRepository.Update(refreshToken);
                await _unitOfWork.SaveChangesAsync();
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error revoking token");
            return false;
        }
    }

    public string GenerateJwtToken(int userId, string email)
    {
        return _jwtTokenService.GenerateAccessToken(userId, email);
    }

    public string GenerateRefreshToken()
    {
        return _jwtTokenService.GenerateRefreshToken();
    }
}
