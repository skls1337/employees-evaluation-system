using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    public record Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CompanyName {get;set;}

        public Company()
        {
            Id =ObjectId.GenerateNewId().ToString();
        }
    }
}