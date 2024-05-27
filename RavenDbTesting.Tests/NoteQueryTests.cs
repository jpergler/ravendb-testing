using RavenDbTesting.Logic;
using RavenDbTesting.Tests.TestDrivers;

namespace RavenDbTesting.Tests;

public class NoteQueryTests : NotesTestDriver
{
    [Fact]
    public void GetNotes_ReturnsOnlyNotesForCurrentUser()
    {
        // Arrange
        var sut = new NoteService(DocumentStore, MainUserProvider);
        var expectedCount = 2;

        // Act
        var notes = sut.GetNotes();

        // Assert
        Assert.Equal(expectedCount, notes.Count);

        foreach (var note in notes)
        {
            Assert.Equal(MainUser.Id, note.OwnerId);
        }
    }

    [Fact]
    public void GetNote_ReturnsNoteForCurrentUser()
    {
        // Arrange
        var sut = new NoteService(DocumentStore, MainUserProvider);
        var expectedNote = MainUserNote1;

        // Act
        var note = sut.GetNote(MainUserNote1.Id);

        // Assert
        Assert.NotNull(note);
        Assert.Equal(expectedNote.Id, note.Id);
        Assert.Equal(expectedNote.Title, note.Title);
        Assert.Equal(expectedNote.Content, note.Content);
        Assert.Equal(expectedNote.OwnerId, note.OwnerId);
    }

    [Fact]
    public void GetNote_ReturnsNullForNonExistentNote()
    {
        // Arrange
        var sut = new NoteService(DocumentStore, MainUserProvider);

        // Act
        var note = sut.GetNote("notes/4");

        // Assert
        Assert.Null(note);
    }

    [Fact]
    public void GetNote_ReturnsNullForNoteOfAnotherUser()
    {
        // Arrange
        var sut = new NoteService(DocumentStore, MainUserProvider);

        // Act
        var note = sut.GetNote(OtherUserNote.Id);

        // Assert
        Assert.Null(note);
    }
}