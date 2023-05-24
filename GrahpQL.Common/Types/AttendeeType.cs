using Backend.Common.Data;
using GraphQL.Common.Extensions;
using GraphQL.Common.Loaders;
using HotChocolate.Types;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Common.Types
{
    public class AttendeeType : ObjectType<Attendee>
    {
        protected override void Configure(IObjectTypeDescriptor<Attendee> descriptor)
        {
            descriptor.Field(t => t.Id).Type<IdType>().ID(nameof(Attendee));

            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode(async (ctx, id) => await ctx.RequestServices.GetService<AttendeeByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.SessionAttendees)
                .ResolveWith<AttendeeResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("sessions");
        }

        private class AttendeeResolvers
        {
            public async Task<IEnumerable<Session>> GetSessionsAsync
            (
                [Parent] Attendee attendee,
                [Service(ServiceKind.Resolver)] ApplicationDbContext dbContext,
                SessionByIdDataLoader sessionById,
                CancellationToken cancellationToken)
            {
                int[] speakerIds = await dbContext.Attendees
                    .Where(a => a.Id == attendee.Id)
                    .Include(a => a.SessionAttendees)
                    .SelectMany(a => a.SessionAttendees.Select(t => t.SessionId))
                    .ToArrayAsync(cancellationToken: cancellationToken);

                return await sessionById.LoadAsync(speakerIds, cancellationToken);
            }
        }
    }
}