using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectRanking.Data;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController: Controller
{
    private readonly ApplicationDbContext _context;
   
    public TestController(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ApplicationUser>>> GetAllEmployees()
    {
        var allEmployees = await _context.ApplicationUsers.ToListAsync();
      
        return Ok(allEmployees);
    }
}