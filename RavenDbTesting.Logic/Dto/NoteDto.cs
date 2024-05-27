namespace RavenDbTesting.Logic.Dto;

public record NoteDto(string Id, string Title, string? Content, string OwnerId);