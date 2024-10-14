using System;

namespace CosmosMongoDBApi.DTO;

public class AuthorBookDTO
{
        public required string AuthorName { get; set; }
        public required string AuthorBio { get; set; }
        public required string BookTitle { get; set; }
        public DateOnly PublishedYear { get; set; }
        public required string AuthorId { get; set; }
}
