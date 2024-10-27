using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ObjectRanking.Models.Entities;

public class RankingSurvey
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public double? RelationCap { get; set; } = 0.1f;
    
    [ValidateNever]
    public ICollection<UserSurvey> UserSurveys { get; set; }
    [ValidateNever]
    public ICollection<Tour> Tours { get; set; }
}