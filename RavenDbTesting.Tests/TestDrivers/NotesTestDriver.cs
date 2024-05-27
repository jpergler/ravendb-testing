using Raven.Client.Documents;
using RavenDbTesting.Data.Indexes;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Data.Setup;

namespace RavenDbTesting.Tests.TestDrivers;

public class NotesTestDriver : UserTestDriver
{
    protected static readonly Note MainUserNote1 = new Note
    {
        Id = "notes/1",
        Title = "Test Note",
        Content = "This is a test note.",
        OwnerId = MainUser.Id
    };

    protected static readonly Note MainUserNote2 = new Note
    {
        Id = "notes/2",
        Title = "Another Note",
        Content = "This is another test note.",
        OwnerId = MainUser.Id
    };

    protected static readonly Note OtherUserNote = new Note
    {
        Id = "notes/3",
        Title = "Third Note",
        Content = "This is a note from another user.",
        OwnerId = OtherUser.Id
    };

    private readonly ICollection<Note> notes = new List<Note>
    {
        MainUserNote1, MainUserNote2, OtherUserNote
    };

    protected override void SetupDatabase(IDocumentStore documentStore)
    {
        documentStore.DeployIndexes();

        using var session = documentStore.OpenSession();
        session.Store(MainUser);
        session.Store(OtherUser);

        foreach (var note in notes)
        {
            session.Store(note);
        }

        session.SaveChanges();
    }
}