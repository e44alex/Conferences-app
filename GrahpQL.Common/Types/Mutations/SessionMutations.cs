using Backend.Common.Data;
using GraphQL.Common.Models;
using GraphQL.Common.Models.Input;
using GraphQL.Common.Models.Payload;
using GraphQL.Common.Subscriptions;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace GraphQL.Common.Types.Mutations
{
    [ExtendObjectType("Mutation")]
    public class SessionMutations
    {
        public async Task<AddSessionPayload> AddSessionAsync
        (
            Models.Input.Session input,
            [Service(ServiceKind.Resolver)] ApplicationDbContext dbContext,
            CancellationToken cancellationToken
        )
        {
            if (string.IsNullOrEmpty(input.Title))
            {
                return new AddSessionPayload(new UserError("Title cannot be empty.", "TITLE_EMPTY"));
            }

            if (input.SpeakerIds.Count == 0)
            {
                return new AddSessionPayload(
                    new UserError("No speaker assigned.", "NO_SPEAKER"));
            }

            var session = new Backend.Common.Data.Session
            {
                Title = input.Title,
                Abstract = input.Abstract,
            };

            foreach (int speakerId in input.SpeakerIds)
            {
                session.SessionSpeakers.Add(new SessionSpeaker
                {
                    SpeakerId = speakerId
                });
            }

            dbContext.Sessions.Add(session);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new AddSessionPayload(session);
        }

        public async Task<ScheduleSessionPayload> ScheduleSessionAsync
        (
            ScheduleSessionInput input,
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            [Service] ITopicEventSender eventSender
        )
        {
            if (input.EndTime < input.StartTime)
            {
                return new ScheduleSessionPayload(
                    new UserError("endTime has to be larger than startTime.", "END_TIME_INVALID"));
            }

            Backend.Common.Data.Session? session = await context.Sessions.FindAsync(input.SessionId);

            if (session is null)
            {
                return new ScheduleSessionPayload(
                    new UserError("Session not found.", "SESSION_NOT_FOUND"));
            }

            session.TrackId = input.TrackId;
            session.StartTime = input.StartTime;
            session.EndTime = input.EndTime;

            await context.SaveChangesAsync();

            await eventSender.SendAsync(
                nameof(SessionSubscriptions.OnSessionScheduledAsync),
                session.Id);

            return new ScheduleSessionPayload(session);
        }
    }
}