using BackEnd.Data;
using BackEnd.GraphQL.Models;
using HotChocolate;
using System.Threading.Tasks;

namespace BackEnd.GraphQL.Queries
{
    public class Mutation
    {
        public async Task<AddSpeakerPayload> AddSpeakerAsync(
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