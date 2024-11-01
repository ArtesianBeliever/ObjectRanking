using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectRanking.Data;
using ObjectRanking.Models.Entities;
using ObjectRanking.Interfaces;
using ObjectRanking.Services;

namespace ObjectRanking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : Controller
{
    private ApplicationDbContext _context;
    private ITestService _testService;

    public TestController(ApplicationDbContext dbContext, ITestService testService)
    {
        _context = dbContext;
        _testService = testService;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<ApplicationUser>>> GetAllEmployees()
    {
        var allEmployees = await _context.ApplicationUsers.ToListAsync();

        return Ok(allEmployees);
    }
    [HttpGet(Name = "GetFirstEmployee")]
    public async Task<ActionResult<ApplicationUser>> GetApplicationUserFirst()
    {
        var user = await _testService.GetUser();
        
        return Ok(user);
    }
}