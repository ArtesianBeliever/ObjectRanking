using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ObjectRanking.Data;
using ObjectRanking.Interfaces;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Services;

public class TestService : ITestService
{
    ApplicationDbContext _context;
    public TestService(ApplicationDbContext context)
    {
        _context = context;
    }
    public string GetTime()
    {
        return DateTime.Now.ToLongTimeString();
    }

    public Task<ApplicationUser?> GetUser()
    {
        return _context.ApplicationUsers.FirstOrDefaultAsync();
    }
}