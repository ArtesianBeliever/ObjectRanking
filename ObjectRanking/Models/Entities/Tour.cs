using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ObjectRanking.Models.Entities;

public class Tour
{
    public Guid Id { get; set; }
    public required Guid SurveyId { get; set; }
    public required RankingSurvey Survey { get; set; }
    
    public int TourNumber { get; set; }
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = DateTime.Now.AddDays(1);
    public string? UserRankTableUrl { get; set; }
    public string? DistanceMatrixUrl { get; set; }
    public int? DistanceMedian { get; set; }
    public int? DistanceSum { get; set; }
    public string? RelationMatrixUrl { get; set; }
    public string? RelationGraphUrl { get; set; }
    public string? Notes { get; set; }
    
    [ValidateNever]
    public ICollection<UserRanking> UserRankings { get; set; } = new List<UserRanking>();
}