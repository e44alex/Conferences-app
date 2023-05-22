using Backend.Common.Data;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Loaders
{
    public class SpeakerByIdLoader : BatchDataLoader<int, Speaker>
    {
        private readonly ApplicationDbContext _dbContext;

        public SpeakerByIdLoader
        (
            IBatchScheduler batchScheduler,
            ApplicationDbContext dbContext,
            DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IReadOnlyDictionary<int, Speaker>> LoadBatchAsync
        (
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Speakers
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}