using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ObjectRanking.Data;
using ObjectRanking.Interfaces;
using ObjectRanking.Models;
using ObjectRanking.Models.Dto;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    private IConfiguration _config;
    
    private IHttpContextAccessor _httpContext;
    private IPasswordHasher _passwordHasher { get; }
   
    public LoginController(ApplicationDbContext dbContext, IConfiguration config, IPasswordHasher passwordHasher
    , IHttpContextAccessor  httpContext)
    {
        _config = config;
        _context = dbContext;
        _passwordHasher = passwordHasher;
        _httpContext = httpContext;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var user = Authenticate(loginRequest);

        if (user != null)
        {
            var token = Generate(user);
            HttpContext.Response.Cookies.Append("Token", token);
            return Ok(token);
        }
        return NotFound("User not found");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetPassword(string password)
    {
        return Ok(_passwordHasher.Generate(password));
    }
    
    [HttpPut]
    public async Task<IActionResult> Register(UserDto dto)
    {
        var newUser = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordHash = _passwordHasher.Generate(dto.Password),
            Name = dto.Name,
        };
        _context.ApplicationUsers.Add(newUser);
        await _context.SaveChangesAsync();
        return Ok(newUser);
    }
    private ApplicationUser Authenticate(LoginRequest loginRequest)
    {
        var currentUser = _context.ApplicationUsers.FirstOrDefault(u 
            => u.Email.ToLower() == loginRequest.Email.ToLower());
        
        if (currentUser != null)
        {
            var verify = _passwordHasher.Verify(loginRequest.Password, currentUser.PasswordHash);
            if (verify)
            {
                return currentUser;
            }
        }
        return null;
    }
    
    private string Generate(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtOptions:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };
        var token = new JwtSecurityToken(_config["JwtOptions:Issuer"],
            _config["JwtOptions:Audience"],
            claims,
            expires: DateTime.Now.AddHours(Int32.Parse(_config["JwtOptions:ExpiresHours"])),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}