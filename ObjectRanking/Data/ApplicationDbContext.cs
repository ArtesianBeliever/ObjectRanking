using Microsoft.EntityFrameworkCore;
using ObjectRanking.Models.Entities;
using Object = ObjectRanking.Models.Entities.Object;

namespace ObjectRanking.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Object> Objects { get; set; }
    public DbSet<RankingSurvey> RankingSurveys { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<UserRanking> UserRankings { get; set; }
    public DbSet<UserSurvey> UserSurveys { get; set; }
    public DbSet<UserSurveyRole> UserSurveyRoles { get; set; }
    public DbSet<UserType> Types { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<UserSurvey>().HasKey(us => new { us.UserId, us.SurveyId, us.RoleId });
        
        builder.Entity<UserSurvey>().HasOne(us => us.User).WithMany(us => us.UserSurveys).HasForeignKey(us => us.UserId);
        
        builder.Entity<UserSurvey>().HasOne(us => us.Survey).WithMany(us => us.UserSurveys).HasForeignKey(us => us.SurveyId);
        
        builder.Entity<UserSurvey>().HasOne(us => us.SurveyRole).WithMany(us => us.UserSurveys).HasForeignKey(us => us.RoleId);

        builder.Entity<UserRankingObject>().HasKey(ro => new { ro.UserRankingId, ro.ObjectId });
        
        builder.Entity<Tour>().HasOne<RankingSurvey>(x => x.Survey).WithMany(x => x.Tours).HasForeignKey(x => x.SurveyId);

        builder.Entity<UserRanking>().HasKey(ur => new {ur.UserId, ur.TourId, ur.SurveyId });

        builder.Entity<UserRanking>().HasOne(ur => ur.Tour).WithMany(ur => ur.UserRankings ).HasForeignKey(ur => ur.TourId);
        
    }
}