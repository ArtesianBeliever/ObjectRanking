using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ObjectRanking.Interfaces;
using ObjectRanking.Models;

namespace ObjectRanking;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;

    public string GenerateToken(UserModel user)
    {
        Claim[] claims = [new("userId", user.Id.ToString())];
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddHours(_jwtOptions.ExpireHours)
            );
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenString;
    }
}

