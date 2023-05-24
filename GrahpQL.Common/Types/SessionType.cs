using Backend.Common.Data;
using GraphQL.Common.Extensions;
using GraphQL.Common.Loaders;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Common.Types
{
    public class SessionType : ObjectType<Session>
    {
        protected override void Configure(IObjectTypeDescriptor<Session> descriptor)
        {
            descriptor.Field(t => t.Id).Type<IdType>().ID(nameof(Session));

            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode(async (ctx, id) => await ctx.RequestServices.GetService<SessionByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.SessionSpeakers)
                .ResolveWith<SessionResolvers>(t => t.GetSpeakersAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("speakers");

            descriptor
                .Field(t => t.SessionAttendees)
                .ResolveWith<SessionResolvers>(t => t.GetAttendeesAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("attendees");

            descriptor
                .Field(t => t.Track)
                .ResolveWith<SessionResolvers>(t => t.GetTrackAsync(default!, default!, default));

            descriptor
                .Field(t => t.TrackId)
                .ID(nameof(Track))
                .Name("track");
        }

        private class SessionResolvers
        {
            public async Task<IEnumerable<Speaker>> GetSpeakersAsync
            (
                [Parent] Session session,
                [Service(ServiceKind.Resolver)] ApplicationDbContext dbContext,
                SpeakerByIdDataLoader speakerByIdData,
                CancellationToken cancellationToken)
            {
                int[] speakerIds = await dbContext.Sessions
                    .Where(s => s.Id == session.Id)
                    .Include(s => s.SessionSpeakers)
                    .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
                    .ToArrayAsync();

                return await speakerByIdData.LoadAsync(speakerIds, cancellationToken);
            }

            public async Task<IEnumerable<Attendee>> GetAttendeesAsync
            (
                [Parent] Session session,
                [Service(ServiceKind.Resolver)] ApplicationDbContext dbContext,
                AttendeeByIdDataLoader attendeeById,
                CancellationToken cancellationToken)
            {
                int[] attendeeIds = await dbContext.Sessions
                    .Where(s => s.Id == session.Id)
                    .Include(session => session.SessionAttendees)
                    .SelectMany(session => session.SessionAttendees.Select(t => t.AttendeeId))
                    .ToArrayAsync(cancellationToken);

                return await attendeeById.LoadAsync(attendeeIds, cancellationToken);
            }

            public async Task<Track?> GetTrackAsync
            (
                [Parent] Session session,
                TrackByIdDataLoader trackById,
                CancellationToken cancellationToken)
            {
                if (session.TrackId is null)
                {
                    return null;
                }

                return await trackById.LoadAsync(session.TrackId.Value, cancellationToken);
            }
        }
    }
}