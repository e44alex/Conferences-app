using Backend.Common.Data;
using GraphQL.Common.Models;
using GraphQL.Common.Models.Input;
using GraphQL.Common.Models.Payload;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Types.Mutations
{
    [ExtendObjectType("Mutation")]
    public class AttendeeMutations
    {
        public async Task<RegisterAttendeePayload> RegisterAttendeeAsync
        (
            RegisterAttendeeInput input,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            CancellationToken cancellationToken
        )
        {
            var attendee = new Attendee
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Username = input.UserName,
                EmailAddress = input.EmailAddress
            };

            context.Attendees.Add(attendee);

            await context.SaveChangesAsync(cancellationToken);

            return new RegisterAttendeePayload(attendee);
        }

        public async Task<CheckInAttendeePayload> CheckInAttendeeAsync
        (
            CheckInAttendeeInput input,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            CancellationToken cancellationToken
        )
        {
            Attendee? attendee = await context.Attendees.FirstOrDefaultAsync(
                t => t.Id == input.AttendeeId, cancellationToken);

            if (attendee is null)
            {
                return new CheckInAttendeePayload(
                    new UserError("Attendee not found.", "ATTENDEE_NOT_FOUND"));
            }

            attendee.SessionAttendees.Add(
                new SessionAttendee
                {
                    SessionId = input.SessionId
                });

            await context.SaveChangesAsync(cancellationToken);

            return new CheckInAttendeePayload(attendee, input.SessionId);
        }
    }
}