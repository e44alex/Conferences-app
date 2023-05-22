using Backend.Common.DTO;
using Attendee = Backend.Common.Data.Attendee;
using Session = Backend.Common.Data.Session;
using Speaker = Backend.Common.Data.Speaker;

namespace Backend.Common.Infrastructure
{
    public static class EntityExtensions
    {
        public static SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new SpeakerResponse
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?.Select(ss => new DTO.Session
                {
                    Id = ss.SessionId,
                    Title = ss.Session.Title
                }).ToList()
            };

        public static SessionResponse MapSessionResponse(this Session session) =>
            new SessionResponse()
            {
                Id = session.Id,
                Abstract = session.Abstract,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Title = session.Title,
                Speakers = session.SessionSpeakers?.Select(ss => new DTO.Speaker
                {
                    Id = ss.SpeakerId,
                    Name = ss.Speaker.Name
                }).ToList(),
                TrackId = session.TrackId,
                Track = new Track
                {
                    Id = session?.TrackId ?? 0,
                    Name = session?.Track?.Name ?? string.Empty
                }
            };

        public static AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
            new AttendeeResponse
            {
                Username = attendee.Username,
                Id = attendee.Id,
                EmailAddress = attendee.EmailAddress,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                Sessions = attendee.SessionAttendees?
                    .Select(sa =>
                        new DTO.Session
                        {
                            Id = sa.SessionId,
                            Title = sa.Session.Title,
                            StartTime = sa.Session.StartTime,
                            EndTime = sa.Session.EndTime
                        })
                    .ToList()
            };
    }
}