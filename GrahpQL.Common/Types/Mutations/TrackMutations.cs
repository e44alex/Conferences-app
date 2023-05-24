using Backend.Common.Data;
using GraphQL.Common.Models;
using GraphQL.Common.Models.Input;
using GraphQL.Common.Models.Payload;
using HotChocolate;
using HotChocolate.Types;
using Track = Backend.Common.Data.Track;

namespace GraphQL.Common.Types.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TrackMutations
    {
        public async Task<AddTrackPayload> AddTrackAsync
        (
            Models.Input.Track input,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            CancellationToken cancellationToken
        )
        {
            var track = new Track { Name = input.Name };
            context.Tracks.Add(track);

            await context.SaveChangesAsync(cancellationToken);

            return new AddTrackPayload(track);
        }

        public async Task<RenameTrackPayload> RenameTrackAsync
        (
            RenameTrackInput input,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            CancellationToken cancellationToken)
        {
            Track? track = await context.Tracks.FindAsync(input.Id);

            if (track == null)
            {
                return new RenameTrackPayload(new List<UserError> { new UserError("Track is not found", "TRACK_NOT_FOUND") });
            }

            track.Name = input.Name;

            await context.SaveChangesAsync(cancellationToken);

            return new RenameTrackPayload(track);
        }
    }
}