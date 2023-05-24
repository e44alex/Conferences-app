using Backend.Common.Data;
using GraphQL.Common.Models.Payload.Base;

namespace GraphQL.Common.Models.Payload
{
    public class AddSpeakerPayload : SpeakerPayloadBase
    {
        public AddSpeakerPayload(Speaker speaker) : base(speaker)
        {
        }

        public AddSpeakerPayload(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}