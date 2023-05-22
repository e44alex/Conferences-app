namespace Backend.Common.Data
{
    public class Attendee : DTO.Attendee
    {
        public virtual ICollection<SessionAttendee> SessionAttendees { get; set; }
    }
}