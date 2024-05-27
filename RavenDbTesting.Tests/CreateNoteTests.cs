using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic;
using RavenDbTesting.Tests.TestDrivers;

namespace RavenDbTesting.Tests;

public class CreateNoteTests : SingleUserTestDriver
{
    [Fact]
    public async Task AddNote_IsStoredToDatabase()
    {
        // Arrange
        const string title = "Test Note";
        const string content = "This is a test note.";
        var sut = new NoteService(DocumentStore, MainUserProvider);

        // Act
        var id = await sut.CreateNoteAsync(title, content);

        // Assert
        using var session = DocumentStore.OpenAsyncSession();
        var savedNote = await session.LoadAsync<Note>(id);
        Assert.NotNull(savedNote);

        Assert.Equal(title, savedNote.Title);
        Assert.Equal(content, savedNote.Content);
    }

    [Fact]
    public async Task AddNote_ContainsCorrectOwnerId()
    {
        // Arrange
        var sut = new NoteService(DocumentStore, MainUserProvider);

        // Act
        var id = await sut.CreateNoteAsync("Test Note", "This is a test note.");

        // Assert
        using var session = DocumentStore.OpenAsyncSession();
        var savedNote = await session.LoadAsync<Note>(id);
        Assert.Equal(MainUser.Id, savedNote.OwnerId);
    }
}