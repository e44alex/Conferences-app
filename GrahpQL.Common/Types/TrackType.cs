using Backend.Common.Data;
using GraphQL.Common.Extensions;
using GraphQL.Common.Loaders;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Common.Types
{
    public class TrackType : ObjectType<Track>
    {
        protected override void Configure(IObjectTypeDescriptor<Track> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode(async (ctx, id) =>
                    await ctx.RequestServices.GetService<TrackByIdDataLoader>()?.LoadAsync(id, ctx.RequestAborted)!);

            descriptor
                .Field(t => t.Name)
                .UseUpperCase();

            descriptor
                .Field(t => t.Sessions)
                .ResolveWith<TrackResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
                .UsePaging<NonNullType<SessionType>>()
                .Name("sessions");
        }

        private class TrackResolvers
        {
            public async Task<IEnumerable<Session>> GetSessionsAsync
            (
                [Parent] Track track,
                [Service(ServiceKind.Resolver)] ApplicationDbContext dbContext,
                SessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] sessionIds = await dbContext.Sessions
                    .Where(s => s.TrackId == track.Id)
                    .Select(s => s.Id)
                    .ToArrayAsync(cancellationToken);

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}