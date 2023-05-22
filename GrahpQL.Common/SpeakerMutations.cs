using Backend.Common.Data;
using GraphQL.Common.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQL.Common
{
    [ExtendObjectType("Mutation")]
    public class SpeakerMutations
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

    public class SpeakerPayloadBase : Payload
    {
        public Speaker? Speaker { get; }

        protected SpeakerPayloadBase(Speaker speaker)
        {
            Speaker = speaker;
        }

        protected SpeakerPayloadBase(IReadOnlyList<UserError> errors) : base(errors)
        {
        }
    }
}