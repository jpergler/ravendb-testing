using Raven.Client.Documents.Indexes;
using RavenDbTesting.Data.Model;

namespace RavenDbTesting.Data.Indexes;

public class NoteCounts_ByUserId : AbstractIndexCreationTask<Note, NoteCounts_ByUserId.Result>
{
    public class Result
    {
        public string OwnerId { get; set; } = null!;
        public int Count { get; set; }
    }

    public NoteCounts_ByUserId()
    {
        Map = notes => from note in notes
                       select new Result
                       {
                           OwnerId = note.OwnerId,
                           Count = 1
                       };

        Reduce = results => from result in results
                            group result by result.OwnerId
                            into g
                            select new Result
                            {
                                OwnerId = g.Key,
                                Count = g.Sum(x => x.Count)
                            };
    }
}