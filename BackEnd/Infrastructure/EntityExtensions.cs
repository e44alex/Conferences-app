using System.Linq;
using ConferenceDTO;
using Attendee = BackEnd.Data.Attendee;
using Session = BackEnd.Data.Session;
using Speaker = BackEnd.Data.Speaker;

namespace BackEnd.Infrastructure
{
    public static class EntityExtensions
    {
        public static ConferenceDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new ConferenceDTO.SpeakerResponse
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?.Select(ss=> 
                    new ConferenceDTO.Session
                    {
                        Id = ss.SessionId,
                        Title = ss.Session.Title
                    }).ToList()
            };

        public static ConferenceDTO.SessionResponse MapSessionResponse(this Session session) =>
            new ConferenceDTO.SessionResponse()
            {
                Id = session.Id,
                Abstract = session.Abstract,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Title = session.Title,
                Speakers = session.SessionSpeakers?.Select(ss => new ConferenceDTO.Speaker()
                {
                    Id = ss.SpeakerId,
                    Name = ss.Speaker.Name
                }).ToList(),
                TrackId = session.TrackId,
                Track = new ConferenceDTO.Track
                {
                    Id = session?.TrackId ?? 0,
                    Name = session.Track?.Name
                }
            };

        public static ConferenceDTO.AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
            new ConferenceDTO.AttendeeResponse
            {
               Username = attendee.Username,
               Id = attendee.Id,
               EmailAddress = attendee.EmailAddress,
               FirstName = attendee.FirstName,
               LastName = attendee.LastName,
               Sessions = attendee.SessionAttendees?
                   .Select(sa =>
                       new ConferenceDTO.Session
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