using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    public record Worker
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public List<Grade> Grades { get; set; }

        public Worker()
        {
            Grades = new List<Grade>();
        }
    }
}