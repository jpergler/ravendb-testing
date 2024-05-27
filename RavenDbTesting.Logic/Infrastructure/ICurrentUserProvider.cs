using System.Diagnostics.CodeAnalysis;

namespace RavenDbTesting.Logic.Infrastructure;

public interface ICurrentUserProvider
{
    public string? CurrentUserId { get; }

    [MemberNotNullWhen(returnValue: true, member: nameof(CurrentUserId))]
    public bool IsAuthenticated => CurrentUserId is not null;
}