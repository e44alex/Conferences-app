using Backend.Common.Data;
using GraphQL.Common.Models;
using HotChocolate;

namespace GraphQL.Common.Queries
{
    public class Mutation
    {
        public async Task<AddSpeakerPayload> AddSpeakerAsync
        (
            AddSpeakerInput input,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                Bio = input.Bio,
                WebSite = input.WebSite
            };

            context.Speakers.Add(speaker);
            await context.SaveChangesAsync();

            return new AddSpeakerPayload(speaker);
        }
    }
}