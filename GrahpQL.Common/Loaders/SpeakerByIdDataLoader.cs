using Backend.Common.Data;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Loaders
{
    public class SpeakerByIdDataLoader : BatchDataLoader<int, Speaker>
    {
        private readonly ApplicationDbContext _dbContext;

        public SpeakerByIdDataLoader
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