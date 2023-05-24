using Backend.Common.Data;
using GraphQL.Common.Models.Payload;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.Common.Types.Mutations
{
    [ExtendObjectType("Mutation")]
    public class SpeakerMutations
    {
        public async Task<AddSpeakerPayload> AddSpeakerAsync
        (
            Models.Input.Speaker input,
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