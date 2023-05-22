namespace GraphQL.Common.Models
{
    public record AddSpeakerInput
    (
        string Name,
        string? Bio,
        string? WebSite
    );
}