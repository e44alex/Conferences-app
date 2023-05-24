using Backend.Common.Data;

namespace GraphQL.Common.Models.Payload.Base
{
    public class SpeakerPayloadBase : Abstract.Payload
    {
        public Speaker? Speaker { get; }

        protected SpeakerPayloadBase(Speaker speaker)
        {
            Speaker = speaker;
        }

        protected SpeakerPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}