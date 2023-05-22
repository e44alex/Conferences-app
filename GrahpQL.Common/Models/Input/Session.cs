using HotChocolate.Types.Relay;

namespace GraphQL.Common.Models.Input
{
    public record Session
    (
        string Title,
        string? Abstract,
        [ID(nameof(Speaker))] IReadOnlyList<int> SpeakerIds
    );

    public record ScheduleSessionInput
    (
        [ID(nameof(Session))] int SessionId,
        [ID(nameof(Track))] int TrackId,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime
    );
}