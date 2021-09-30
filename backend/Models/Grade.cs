using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    public record Grade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        [Range(1,10)]
        public int CommunicationLevel { get; set; }
        [Required]
        [Range(1,10)]
        public int ServiceQualityLevel { get; set; }
        [Required]
        [Range(1,10)]
        public int TeamWorkLevel { get; set; }
        [Required]
        [Range(1,10)]
        public int ImplicationLevel { get; set; }
        public DateTime Timestamp { get; set; }

        public Grade()
        {
            Id = ObjectId.GenerateNewId().ToString();
            Timestamp = DateTime.UtcNow;
        }
    }
}