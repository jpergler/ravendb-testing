namespace RavenDbTesting.Data.Model;

public class NoteEvent
{
    public required string Id { get; set; }
    public required string NoteId { get; set; }
    public required string UserId { get; set; }
    public required NoteEventType Type { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}