using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ObjectRanking.Models.Entities;

public class UserSurveyRole
{
    public int Id { get; set; }
    public string? RoleName { get; set; }
    
    [ValidateNever]
    public ICollection<UserSurvey> UserSurveys { get; set; } = new List<UserSurvey>();
}