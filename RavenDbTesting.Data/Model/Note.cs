namespace RavenDbTesting.Data.Model;

public class Note
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public required string OwnerId { get; set; }
}