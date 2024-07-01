using System.Security.Claims;
using Dnct.Application.Models.Jwt;
using Dnct.Domain.Entities.User;

namespace Dnct.Application.Contracts;

public interface IJwtService
{
    Task<AccessToken> GenerateAsync(User user);
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    Task<AccessToken> GenerateByPhoneNumberAsync(string phoneNumber);
    Task<AccessToken> RefreshToken(Guid refreshTokenId);
}