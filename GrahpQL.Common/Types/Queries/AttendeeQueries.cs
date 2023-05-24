using Backend.Common.Data;
using GraphQL.Common.Loaders;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace GraphQL.Common.Types.Queries
{
    [ExtendObjectType("Query")]
    public class AttendeeQueries
    {
        [UsePaging]
        public IQueryable<Attendee> GetAttendees
        (
            [Service(ServiceKind.Resolver)] ApplicationDbContext context) =>
            context.Attendees;

        public Task<Attendee> GetAttendeeByIdAsync
        (
            [ID(nameof(Attendee))] int id,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken
        ) =>
            attendeeById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Attendee>> GetAttendeesByIdAsync
        (
            [ID(nameof(Attendee))] int[] ids,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken
        ) =>
            await attendeeById.LoadAsync(ids, cancellationToken);
    }
}