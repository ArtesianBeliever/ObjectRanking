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
using ObjectRanking.Services;

namespace ObjectRanking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private IPasswordHasher _passwordHasher { get; }
    private ITokenService _tokenService;
    private IUserService _userService;
   
    public LoginController(IPasswordHasher passwordHasher
    , ITokenService tokenService, IUserService userService)
    {
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost(Name = "Login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var user = await _userService.Authenticate(loginRequest);

        if (user != null)
        {
            var token = _tokenService.Generate(user);
            HttpContext.Response.Cookies.Append("Token", token);
            return Ok(token);
        }
        return NotFound("User not found");
    }
    
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserDto dto)
    {
        var newUser = await _userService.Register(dto);
        return Ok(newUser);
    }

}