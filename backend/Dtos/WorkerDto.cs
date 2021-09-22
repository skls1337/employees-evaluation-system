using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Dtos
{
    public record WorkerDto
    {
        public string Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        public Company Company { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        public List<Grade> Grades { get; set; }
        public WorkerDto()
        {
            Grades = new List<Grade>();
        }
    }
}