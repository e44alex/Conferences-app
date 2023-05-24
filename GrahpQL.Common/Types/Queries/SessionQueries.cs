using Backend.Common.Data;
using GraphQL.Common.Loaders;
using GraphQL.Common.Models.Input;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Session = Backend.Common.Data.Session;

namespace GraphQL.Common.Types.Queries
{
    [ExtendObjectType("Query")]
    public class SessionQueries
    {
        [UsePaging(typeof(NonNullType<SessionType>))]
        [UseFiltering(typeof(SessionFilterInputType))]
        [UseSorting]
        public IQueryable<Session> GetSessions([Service(ServiceKind.Resolver)] ApplicationDbContext context) =>
            context.Sessions;

        public Task<Session> GetSessionAsync
        (
            [ID(nameof(Session))] int id,
            SessionByIdDataLoader dataLoader,
            CancellationToken cancellationToken
        ) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}