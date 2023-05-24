using HotChocolate.Data.Filters;
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

    public class SessionFilterInputType : FilterInputType<Backend.Common.Data.Session>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Backend.Common.Data.Session> descriptor)
        {
            descriptor.Ignore(t => t.Id);
            descriptor.Ignore(t => t.TrackId);
        }
    }
}