using Raven.Client.Documents;
using RavenDbTesting.Data;
using RavenDbTesting.Data.Indexes;
using RavenDbTesting.Data.Model;
using RavenDbTesting.Logic.Dto;
using RavenDbTesting.Logic.Extensions;
using RavenDbTesting.Logic.Infrastructure;

namespace RavenDbTesting.Logic;

public class NoteService(IDocumentStore documentStore, ICurrentUserProvider currentUserProvider)
{
    public async Task<string> CreateNoteAsync(string title, string content)
    {
        using var session = documentStore.OpenAsyncSession();

        await session.EnsureUserExists(currentUserProvider.RequiredUserId);

        var note = new Note
        {
            Id = IdHelper.CreateByRavenDb,
            Title = title,
            Content = content,
            OwnerId = currentUserProvider.RequiredUserId
        };

        await session.StoreAsync(note);
        await session.SaveChangesAsync();
        return note.Id;
    }

    public ICollection<NoteDto> GetNotes()
    {
        var userId = currentUserProvider.RequiredUserId;

        using var session = documentStore.OpenSession();
        var notes = session.Query<Note>()
                           .Where(note => note.OwnerId == userId)
                           .ProjectInto<NoteDto>()
                           .ToList();

        return notes;
    }

    public NoteDto? GetNote(string id)
    {
        using var session = documentStore.OpenSession();
        var note = session.Load<Note>(id);

        if (note is null || note.OwnerId != currentUserProvider.RequiredUserId) return null;

        return new NoteDto(note.Id, note.Title, note.Content, note.OwnerId);
    }

    public async Task<Dictionary<string, int>> GetNoteCountByUsers()
    {
        using var session = documentStore.OpenAsyncSession();
        var result = await session.Query<NoteCounts_ByUserId.Result, NoteCounts_ByUserId>()
                                  .ToListAsync();

        return result.ToDictionary(x => x.OwnerId, x => x.Count);
    }
}