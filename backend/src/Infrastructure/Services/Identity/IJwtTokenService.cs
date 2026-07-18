namespace JAS.ECommerce.Infrastructure.Services;

public interface IJwtTokenService
{
    string GenerateAccessToken(int userId, string email);
    string GenerateRefreshToken();
    (int UserId, string Email) ValidateToken(string token);
    bool IsTokenExpired(string token);
}
