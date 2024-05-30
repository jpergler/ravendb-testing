using Raven.Client.Documents;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic;
using RavenDbTesting.Tests.TestDrivers;

namespace RavenDbTesting.Tests;

public class NoteEventTests : UserTestDriver
{
    [Fact]
    public async Task AddNoteEvent_CreatesEventInDatabase()
    {
        // Arrange
        var sut = new NoteService(DocumentStore, MainUserProvider);

        // Act
        var noteId = await sut.CreateNoteAsync("Test Note", "This is a test note.");

        // Assert
        WaitForIndexing(DocumentStore);

        using var session = DocumentStore.OpenAsyncSession();
        var noteEvent = await session.Query<NoteEvent>()
                                     .Where(e => e.NoteId == noteId)
                                     .SingleAsync();

        Assert.NotNull(noteEvent);
        Assert.Equal(NoteEventType.Created, noteEvent.Type);
        Assert.Equal(MainUser.Id, noteEvent.UserId);
    }
}