using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectRanking.Data;
using ObjectRanking.Models.Entities;
using ObjectRanking.Services;

namespace ObjectRanking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public UserController(ApplicationDbContext context)
    {
        _context = context;
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
        var curentUser = GetCurrentUser();
        
        return Ok(curentUser);
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