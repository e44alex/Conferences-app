using Backend.Common.Data;
using GraphQL.Common.Loaders;
using GraphQL.Common.Models.Payload.Base;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Models.Payload
{
    public class ScheduleSessionPayload : SessionPayloadBase
    {
        public ScheduleSessionPayload(Session session)
            : base(session)
        {
        }

        public ScheduleSessionPayload(UserError error)
            : base(new[] { error })
        {
        }

        public async Task<Track?> GetTrackAsync
        (
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken
        )
        {
            if (Session is null)
            {
                return null;
            }

            return await trackById.LoadAsync(Session.Id, cancellationToken);
        }

        public async Task<IEnumerable<Speaker>?> GetSpeakersAsync
        (
            [Service(ServiceKind.Resolver)] ApplicationDbContext dbContext,
            SpeakerByIdDataLoader speakerById,
            CancellationToken cancellationToken
        )
        {
            if (Session is null)
            {
                return null;
            }

            int[] speakerIds = await dbContext.Sessions
                .Where(s => s.Id == Session.Id)
                .Include(s => s.SessionSpeakers)
                .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
                .ToArrayAsync(cancellationToken);

            return await speakerById.LoadAsync(speakerIds, cancellationToken);
        }
    }
}