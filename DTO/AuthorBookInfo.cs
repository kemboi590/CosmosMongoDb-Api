using System;

namespace CosmosMongoDBApi.DTO;

public class AuthorBookInfo
{
    public string? AuthorName { get; set; }
    public string? AuthorBio { get; set; }
    public string? BookTitle { get; set; }
    public int PublishedYear { get; set; }

}
