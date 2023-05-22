using Microsoft.EntityFrameworkCore;

namespace Backend.Common.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>().HasIndex(a => a.Username).IsUnique();

            modelBuilder.Entity<SessionAttendee>().HasKey(a => new { a.SessionId, a.AttendeeId });

            modelBuilder.Entity<SessionSpeaker>().HasKey(a => new { a.SessionId, a.SpeakerId });


        }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Attendee> Attendees { get; set; }
    }
}
