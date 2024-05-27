using RavenDbTesting.Logic;
using RavenDbTesting.Tests.TestDrivers;

namespace RavenDbTesting.Tests;

public class StatisticsTests : NotesTestDriver
{
    [Fact]
    public async Task GetStatistics_ReturnsCorrectStatistics()
    {
        // Arrange
        var sut = new NoteService(DocumentStore, MainUserProvider);
        WaitForIndexing(DocumentStore);

        // Act
        var statistics = await sut.GetNoteCountByUsers();

        // Assert
        Assert.Equal(2, statistics.Count);

        Assert.Equal(2, statistics[MainUser.Id]);
        Assert.Equal(1, statistics[OtherUser.Id]);
    }
}