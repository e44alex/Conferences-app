using Backend.Common.Data;
using GraphQL.Common.Loaders;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Common.Queries
{
    public class Query
    {
        public Task<List<Speaker>> GetSpeakers([Service(ServiceKind.Resolver)] ApplicationDbContext context) => context.Speakers.ToListAsync();

        public Task<Speaker> GetSpeakerAsync
        (
            int id,
            SpeakerByIdLoader dataLoader,
            CancellationToken cancellationToken
        ) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public Task<List<Session>> GetSessions([Service(ServiceKind.Resolver)] ApplicationDbContext context) => context.Sessions.ToListAsync();

        public Task<Session> GetSessionAsync
        (
            int id,
            SessionByIdDataLoader dataLoader,
            CancellationToken cancellationToken
        ) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}