using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Dtos
{
    public record CreateWorkerDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        public List<Grade> Grades { get; set; }

        public CreateWorkerDto()
        {
            Grades = new List<Grade>();
        }
    }
}