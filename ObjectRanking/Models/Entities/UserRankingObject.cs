namespace ObjectRanking.Models.Entities;

public class UserRankingObject
{
    public Guid Id { get; set; }
    public Guid UserRankingId { get; set; }
    public Guid ObjectId { get; set; }
    public int Rank { get; set; }
}