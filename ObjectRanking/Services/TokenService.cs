using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ObjectRanking.Interfaces;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string Generate(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtOptions:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };
        var token = new JwtSecurityToken(config["JwtOptions:Issuer"],
            config["JwtOptions:Audience"],
            claims,
            expires: DateTime.Now.AddHours(Int32.Parse(config["JwtOptions:ExpiresHours"])),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}