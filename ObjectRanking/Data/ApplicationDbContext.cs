using Microsoft.EntityFrameworkCore;
using ObjectRanking.Models.Entities;

namespace ObjectRanking.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
}