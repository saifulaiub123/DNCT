using System.Security.Claims;
using Dnct.Application.Models.Jwt;
using Dnct.Domain.Entities.User;

namespace Dnct.Application.Contracts;

public interface IJwtService
{
    Task<AuthToken> GenerateAsync(User user);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    Task<AuthToken> GenerateByPhoneNumberAsync(string phoneNumber);
    Task<AuthToken> RefreshToken(Guid refreshTokenId);
}