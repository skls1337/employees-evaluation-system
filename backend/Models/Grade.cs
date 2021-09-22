using System;

namespace Backend.Models
{
    public record Grade
    {
        public int CommunicationLevel { get; set; }
        public int ServiceQualityLevel { get; set; }
        public int TeamWorkLevel { get; set; }
        public int ImplicationLevel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}