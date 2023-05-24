using Backend.Common.Data;
using GraphQL.Common.Loaders;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Types.Queries
{
    [ExtendObjectType("Query")]
    public class SpeakerQueries
    {
        [UsePaging]
        public IQueryable<Speaker> GetSpeakers([Service(ServiceKind.Resolver)] ApplicationDbContext context) =>
            context.Speakers.OrderBy(t => t.Name);

        public Task<Speaker> GetSpeakerAsync
        (
            [ID(nameof(Speaker))] int id,
            SpeakerByIdDataLoader dataDataLoader,
            CancellationToken cancellationToken
        ) =>
            dataDataLoader.LoadAsync(id, cancellationToken);
    }
}