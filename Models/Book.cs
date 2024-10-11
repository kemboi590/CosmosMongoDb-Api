using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CosmosMongoDBApi.Models;


public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    public required string Title { get; set; }
    public DateOnly PublishedYear { get; set; }
    public required string AuthorId { get; set; }

}
