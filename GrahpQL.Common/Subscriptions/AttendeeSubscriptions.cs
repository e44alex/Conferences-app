using Backend.Common.Data;
using GraphQL.Common.Types;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace GraphQL.Common.Subscriptions
{
    [ExtendObjectType("Subscription")]
    public class AttendeeSubscriptions
    {
        [Subscribe(With = nameof(SubscribeToOnAttendeeCheckedInAsync))]
        public SessionAttendeeCheckIn OnAttendeeCheckedIn(
            [ID(nameof(Session))] int sessionId,
            [EventMessage] int attendeeId) =>
            new SessionAttendeeCheckIn(attendeeId, sessionId);

        public async ValueTask<ISourceStream<int>> SubscribeToOnAttendeeCheckedInAsync(
            int sessionId,
            [Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken) =>
            await eventReceiver.SubscribeAsync<int>(
                "OnAttendeeCheckedIn_" + sessionId, cancellationToken);
    }
}