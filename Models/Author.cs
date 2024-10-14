using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CosmosMongoDBApi.Models;

public class Author
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    [BsonElement("Name")]
    public required string Name { get; set; }
    [BsonElement("Bio")]
    public required string Bio { get; set; }
}
