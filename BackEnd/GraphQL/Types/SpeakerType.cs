using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using BackEnd.Data;
using BackEnd.GraphQL.Extensions;
using BackEnd.GraphQL.Loaders;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.GraphQL.Types
{
    public class SpeakerType : ObjectType<Speaker>
    {
        protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
        {
            descriptor
                .Field(t => t.SessionSpeakers)
                .ResolveWith<SpeakerResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("sessions");
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