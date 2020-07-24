﻿using System.ComponentModel.DataAnnotations;

namespace ConferenceDTO
{
    public class Attendee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string Username { get; set; }

        
        [StringLength(256)]
        public string EmailAddress { get; set; }
    }
}