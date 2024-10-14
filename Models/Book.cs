using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CosmosMongoDBApi.Models;


public class Book
{
    [BsonId] //makes the property the primary key in the collection
    [BsonRepresentation(BsonType.ObjectId)] //representation in binary format (12 bytes), unique identifier for diocuments 
    public string? Id { get; set; }
    [BsonElement("Title")]
    public required string Title { get; set; }
    [BsonElement("PublishedYear")]
    public DateOnly PublishedYear { get; set; }
    [BsonElement("AuthorId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string AuthorId { get; set; }

}
