using Raven.Client.Documents.Session;
using RavenDbTesting.Data.Model;

namespace RavenDbTesting.Logic.Extensions;

public static class UsersSessionExtensions
{
    public static async Task EnsureUserExists(this IAsyncDocumentSession session, string userId)
    {
        await session.GetRequiredUser(userId);
    }

    public static async Task GetRequiredUser(this IAsyncDocumentSession session, string userId)
    {
        var user = await session.LoadAsync<User>(userId);
        if (user is null) throw new InvalidOperationException("User does not exist.");
    }
}