namespace ObjectRanking.Models.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    
    //public ICollection<UserSurvey> UserSurveys { get; set; } = new List<UserSurvey>();
}