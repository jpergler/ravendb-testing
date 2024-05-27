using Raven.Client.Documents;
using RavenDbTesting.Data;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic.Dto;
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

    public ICollection<NoteDto> GetNotes()
    {
        using var session = documentStore.OpenSession();
        var notes = session.Query<Note>()
                           .Where(note => note.OwnerId == currentUserProvider.CurrentUserId)
                           .ProjectInto<NoteDto>()
                           .ToList();

        return notes;
    }

    public NoteDto? GetNote(string id)
    {
        using var session = documentStore.OpenSession();
        var note = session.Load<Note>(id);

        if (note is null || note.OwnerId != currentUserProvider.CurrentUserId) return null;

        return new NoteDto(note.Id, note.Title, note.Content, note.OwnerId);
    }
}