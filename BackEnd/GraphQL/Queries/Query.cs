using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.GraphQL.Loaders;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.GraphQL.Queries
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