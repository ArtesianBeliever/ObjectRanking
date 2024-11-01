using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectRanking.Data;
using ObjectRanking.Interfaces;
using ObjectRanking.Models.Entities;
using ObjectRanking.Services;

namespace ObjectRanking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;
    
    public UserController(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Public()
    {
        return Ok("Public");
    }

    [HttpGet("Private")]
    [Authorize]
    public IActionResult Private()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var currentUser = _userService.GetCurrentUser(identity);
        
        return Ok(currentUser);
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    private ApplicationUser GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new ApplicationUser()
            {
                Email = userClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value,
                Name = userClaims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier)).Value,
                PasswordHash = "null"
            };
        }
        return null;
    }
}