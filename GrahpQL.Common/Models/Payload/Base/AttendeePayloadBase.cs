using Backend.Common.Data;

namespace GraphQL.Common.Models.Payload.Base
{
    public class AttendeePayloadBase : Abstract.Payload
    {
        protected AttendeePayloadBase(Attendee attendee)
        {
            Attendee = attendee;
        }

        protected AttendeePayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Attendee? Attendee { get; }
    }
}