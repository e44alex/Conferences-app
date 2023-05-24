using Backend.Common.Data;
using HotChocolate.Types.Relay;

namespace GraphQL.Common.Models.Input
{
    public record RegisterAttendeeInput
    (
        string FirstName,
        string LastName,
        string UserName,
        string EmailAddress
    );

    public record CheckInAttendeeInput
    (
        [ID(nameof(Session))] int SessionId,
        [ID(nameof(Attendee))] int AttendeeId
    );
}