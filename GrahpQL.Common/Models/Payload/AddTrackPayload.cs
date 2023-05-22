using Backend.Common.Data;
using GraphQL.Common.Models.Payload.Base;

namespace GraphQL.Common.Models.Payload
{
    public class AddTrackPayload : TrackPayloadBase
    {
        public AddTrackPayload(Track track)
            : base(track)
        {
        }

        public AddTrackPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}