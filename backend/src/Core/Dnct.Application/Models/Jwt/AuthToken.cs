using System.IdentityModel.Tokens.Jwt;

namespace Dnct.Application.Models.Jwt;

public class AuthToken
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }

    public AuthToken(JwtSecurityToken securityToken,string refreshToken="")
    {
        AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        TokenType = "Bearer";
        ExpiresIn = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        RefreshToken = refreshToken;
    }
}