using Raven.Client.Documents;
using RavenDbTesting.Data;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic.Infrastructure;

namespace RavenDbTesting.Logic;

public class NoteService(IDocumentStore documentStore, ICurrentUserProvider currentUserProvider)
{
    public async Task<string> CreateNoteAsync(string title, string content)
    {
        if (!currentUserProvider.IsAuthenticated) throw new InvalidOperationException("You must be authenticated to create a note.");

        var note = new Note
        {
            Id = IdHelper.CreateByRavenDb,
            Title = title,
            Content = content,
            OwnerId = currentUserProvider.CurrentUserId
        };

        using var session = documentStore.OpenAsyncSession();
        await session.StoreAsync(note);
        await session.SaveChangesAsync();
        return note.Id;
    }
}