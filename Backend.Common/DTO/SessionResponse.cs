﻿namespace Backend.Common.DTO
{
    public class SessionResponse : Session
    {
        public Track Track { get; set; }

        public ICollection<Speaker>? Speakers { get; set; } = new List<Speaker>();
    }
}