using BackEnd.Data;

namespace BackEnd.GraphQL.Models
{
    public record AddSpeakerInput
    (
        string Name,
        string Bio,
        string WebSite
    );

    public class AddSpeakerPayload
    {
        public AddSpeakerPayload(Speaker speaker)
        {
            Speaker = speaker;
        }

        public Speaker Speaker { get; }
    }
}