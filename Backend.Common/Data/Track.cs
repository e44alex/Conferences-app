namespace Backend.Common.Data
{
    public class Track : DTO.Track
    {
        public virtual ICollection<Session> Sessions { get; set; }
    }
}