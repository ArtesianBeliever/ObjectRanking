namespace ObjectRanking.Models.Entities;

public class Object
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public required string Name { get; set; }
}