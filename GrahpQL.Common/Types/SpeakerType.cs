using Backend.Common.Data;
using GraphQL.Common.Extensions;
using GraphQL.Common.Loaders;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Common.Types
{
    public class SpeakerType : ObjectType<Speaker>
    {
        protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(type => type.Id)
                .ResolveNode(async (context, id) => await context.RequestServices.GetService<SpeakerByIdDataLoader>().LoadAsync(id, context.RequestAborted));

            descriptor
                .Field(t => t.SessionSpeakers)
                .ResolveWith<SpeakerResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("sessions");

            //descriptor.Field(t => t.Id).ID(nameof(Speaker));
        }

        private class SpeakerResolvers
        {
            public async Task<IEnumerable<Session>> GetSessionsAsync
            (
                [Parent] Speaker speaker,
                [Service(ServiceKind.Resolver)] ApplicationDbContext dbContext,
                SessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] sessionIds = await dbContext.Speakers
                    .Where(s => s.Id == speaker.Id)
                    .Include(s => s.SessionSpeakers)
                    .SelectMany(s => s.SessionSpeakers.Select(t => t.SessionId))
                    .ToArrayAsync(cancellationToken: cancellationToken);

                return await sessionById.LoadAsync(sessionIds, cancellationToken);
            }
        }
    }
}