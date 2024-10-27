namespace ObjectRanking.Models.Entities;

public class UserRanking
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SurveyId { get; set; }
    public Guid TourId { get; set; }
    public string? Comment { get; set; }
    public string? RankMatrixUrl {get; set;}
}