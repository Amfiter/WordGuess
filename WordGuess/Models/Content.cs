using System;

namespace WordGuess.Models
{
    public class Content
    {
        public string content { get; set; }
    }
    public class MessageResponse
    {   public string content { get; set; }
        public bool myself { get; set; } = false;
        public int participantId { get; set; } = 1;
        public DateTime timestamp { get; set; } = DateTime.Now;
        public bool uploaded { get; set; } = true;
        public bool viewed { get; set; } = false;
        public string type { get; set; } = "text";
    }
}