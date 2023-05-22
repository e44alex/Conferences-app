using HotChocolate.Types.Relay;

namespace GraphQL.Common.Models.Input
{
    public record Track(string Name);

    public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);
}