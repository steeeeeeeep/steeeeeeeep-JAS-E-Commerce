namespace JAS.ECommerce.Application.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string? Token, string? RefreshToken, string? Error)> LoginAsync(string email, string password);
    Task<(bool Success, string? Error)> RegisterAsync(string email, string fullName, string phoneNumber, string password);
    Task<(bool Success, string? Token, string? Error)> RefreshTokenAsync(string refreshToken);
    Task<bool> RevokeTokenAsync(string token);
    string GenerateJwtToken(int userId, string email);
    string GenerateRefreshToken();
}
