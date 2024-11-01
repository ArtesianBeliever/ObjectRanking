namespace ObjectRanking.Models.Entities;

public class UserRankingObject
{
    public Guid Id { get; set; }
    public Guid UserRankingId { get; set; }
    public UserRanking UserRanking { get; set; }
    public Guid ObjectId { get; set; }
    public Object Object { get; set; }
    public int Rank { get; set; }
}