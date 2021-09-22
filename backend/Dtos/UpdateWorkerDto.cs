using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Dtos
{
    public record UpdateWorkerDto
    {

        public string FullName { get; set; }
        public string Position { get; set; }
        public Company Company { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        public List<Grade> Grades { get; set; }
    }
}