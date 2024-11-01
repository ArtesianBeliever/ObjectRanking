namespace ObjectRanking.Models.Entities;

public class UserRanking
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    
    public Guid SurveyId { get; set; }
    public RankingSurvey Survey { get; set; }
    
    public Guid TourId { get; set; }
    public Tour Tour { get; set; }
    
    public string? Comment { get; set; }
    public string? RankMatrixUrl {get; set;}
}