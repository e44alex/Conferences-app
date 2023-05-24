using Backend.Common.Data;
using GraphQL.Common.Models.Payload.Base;

namespace GraphQL.Common.Models.Payload
{
    public class RegisterAttendeePayload : AttendeePayloadBase
    {
        public RegisterAttendeePayload(Attendee attendee)
            : base(attendee)
        {
        }

        public RegisterAttendeePayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}