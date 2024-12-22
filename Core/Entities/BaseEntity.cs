namespace Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ArabicName { get; set; }
    public required string Description { get; set; }
    public required string ArabicDescription { get; set; }
}
