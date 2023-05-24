using Backend.Common.Data;
using GraphQL.Common.Loaders;
using HotChocolate;
using System.Linq;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Types.Queries
{
    [ExtendObjectType("Query")]
    public class TrackQueries
    {
        [UsePaging]
        public IQueryable<Track> GetTracks
        (
            [Service(ServiceKind.Resolver)] ApplicationDbContext context
        ) =>
            context.Tracks.OrderBy(t => t.Name);

        [GraphQLDeprecated("Use GetTracks with paging")]
        public async Task<IEnumerable<Track>> GetTracksAsync
        (
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            CancellationToken cancellationToken
        ) =>
            await context.Tracks.OrderBy(t => t.Name).ToListAsync(cancellationToken);

        public Task<Track> GetTrackByNameAsync
        (
            string name,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            context.Tracks.FirstAsync(t => t.Name == name, cancellationToken: cancellationToken);

        public async Task<IEnumerable<Track>> GetTrackByNamesAsync
        (
            string[] names,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
            await context.Tracks.Where(t => names.Contains(t.Name)).ToListAsync(cancellationToken);

        public Task<Track> GetTrackByIdAsync
        (
            [ID(nameof(Track))] int id,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            trackById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Track>> GetTracksByIdAsync
        (
            [ID(nameof(Track))] int[] ids,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
            await trackById.LoadAsync(ids, cancellationToken);
    }
}