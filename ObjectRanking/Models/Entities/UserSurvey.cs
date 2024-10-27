namespace ObjectRanking.Models.Entities;

public class UserSurvey
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
    public Guid SurveyId { get; set; }
    public RankingSurvey Survey { get; set; }
    public int RoleId { get; set; }
    public UserSurveyRole SurveyRole { get; set; }
}