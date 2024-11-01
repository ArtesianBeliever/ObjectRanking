using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ObjectRanking.Models.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    
    [ValidateNever]
    public ICollection<UserSurvey> UserSurveys { get; set; } = new List<UserSurvey>();
}