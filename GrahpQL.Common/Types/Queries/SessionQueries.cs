using Backend.Common.Data;
using GraphQL.Common.Loaders;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Types.Queries
{
    [ExtendObjectType("Query")]
    public class SessionQueries
    {
        public Task<List<Session>> GetSessions([Service(ServiceKind.Resolver)] ApplicationDbContext context) => context.Sessions.ToListAsync();

        public Task<Session> GetSessionAsync
        (
            [ID(nameof(Session))] int id,
            SessionByIdDataLoader dataLoader,
            CancellationToken cancellationToken
        ) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}