using Backend.Common.Data;

namespace GraphQL.Common.Models.Payload.Base
{
    public class TrackPayloadBase : Abstract.Payload
    {
        public TrackPayloadBase(Track track)
        {
            Track = track;
        }

        public TrackPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Track? Track { get; }
    }
}