using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dnct.Application.Contracts;
using Dnct.Application.Contracts.Persistence;
using Dnct.Application.Models.Jwt;
using Dnct.Domain.Entities.User;
using Dnct.Infrastructure.Identity.Identity.Dtos;
using Dnct.Infrastructure.Identity.Identity.Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Dnct.Infrastructure.Identity.Jwt;

public class JwtService : IJwtService
{
    private readonly IdentitySettings _siteSetting;
    private readonly AppUserManager _userManager;
    private IUserClaimsPrincipalFactory<User> _claimsPrincipal;

    private readonly IUnitOfWork _unitOfWork;
    //private readonly AppUserClaimsPrincipleFactory claimsPrincipleFactory;

    public JwtService(IOptions<IdentitySettings> siteSetting, AppUserManager userManager, IUserClaimsPrincipalFactory<User> claimsPrincipal, IUnitOfWork unitOfWork)
    {
        _siteSetting = siteSetting.Value;
        _userManager = userManager;
        _claimsPrincipal = claimsPrincipal;
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthToken> GenerateAsync(User user)
    {
        var secretKey = Encoding.UTF8.GetBytes(_siteSetting.SecretKey); // longer that 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.Encryptkey); //must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


        var claims = await _getClaimsAsync(user);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _siteSetting.Issuer,
            Audience = _siteSetting.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(0),
            Expires = DateTime.Now.AddMinutes(_siteSetting.ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);


        var refreshToken = await _unitOfWork.UserRefreshTokenRepository.CreateToken(user.Id);
        await _unitOfWork.CommitAsync();

        return new AuthToken(securityToken,refreshToken.ToString());
    }

    public Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.SecretKey)),
            ValidateLifetime = false,
            TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSetting.Encryptkey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return Task.FromResult(principal);
    }

    public async Task<AuthToken> GenerateByPhoneNumberAsync(string phoneNumber)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        var result = await this.GenerateAsync(user);
        return result;
    }

    public async Task<AuthToken> RefreshToken(Guid refreshTokenId)
    {
        var refreshToken = await _unitOfWork.UserRefreshTokenRepository.GetTokenWithInvalidation(refreshTokenId);
            
        if (refreshToken is null)
            return null;

        refreshToken.IsValid = false;

        await _unitOfWork.CommitAsync();

        var user = await _unitOfWork.UserRefreshTokenRepository.GetUserByRefreshToken(refreshTokenId);

        if (user is null)
            return null;

        var result = await this.GenerateAsync(user);

        return result;
    }

    private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
    {
        var result = await _claimsPrincipal.CreateAsync(user);
        return result.Claims;
    }
}