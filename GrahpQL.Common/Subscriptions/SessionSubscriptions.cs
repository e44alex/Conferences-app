using Backend.Common.Data;
using GraphQL.Common.Loaders;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.Common.Subscriptions
{
    [ExtendObjectType("Subscription")]
    public class SessionSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Session> OnSessionScheduledAsync
        (
            [EventMessage] int sessionId,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken
        ) => sessionById.LoadAsync(sessionId, cancellationToken);
    }
}