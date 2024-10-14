
using CosmosMongoDBApi.DTO;
using CosmosMongoDBApi.Models;
using CosmosMongoDBApi.Models.DbContext;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


namespace CosmosMongoDBApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{

    private readonly MongoDbContext _context;

    public AuthorsController(MongoDbContext context) //passing the context to the controller
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        var authors = await _context.Authors.Find(author => true).ToListAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(string id)
    {
        var author = await _context.Authors.Find(author => author.Id == id).FirstOrDefaultAsync();
        if (author == null)
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(Author author)
    {
        await _context.Authors.InsertOneAsync(author);
        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(string id, Author updatedAuthor)
    {
        var authorToUpdate = await _context.Authors.Find(author => author.Id == id).FirstOrDefaultAsync();
        if (authorToUpdate is null)
        {
            return NotFound();
        }

        updatedAuthor.Id = authorToUpdate.Id;

        await _context.Authors.ReplaceOneAsync(author => author.Id == id, updatedAuthor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(string id)
    {
        var result = await _context.Authors.DeleteOneAsync(author => author.Id == id);
        if (result.IsAcknowledged && result.DeletedCount > 0)
        {
            return NoContent();
        }
        return NotFound();
    }


    [HttpGet("author-books")]
    public async Task<IActionResult> GetAuthorBookInfo()
    {
        // Fetch all books
        var books = await _context.Books.Find(book => true).ToListAsync();

        // Fetch all authors
        var authors = await _context.Authors.Find(author => true).ToListAsync();

        // Convert authors to a dictionary for quick look-up
        var authorDictionary = authors.ToDictionary(a => a.Id!, a => a);

        // map the books to your DTO using the author dictionary
        var authorBookDTOs = books.Select(book => new AuthorBookDTO
        {
            AuthorName = authorDictionary.TryGetValue(book.AuthorId, out var author) ? author.Name : "Unknown",
            AuthorBio = authorDictionary.TryGetValue(book.AuthorId, out author) ? author.Bio : "No Bio Available",
            BookTitle = book.Title,
            PublishedYear = book.PublishedYear,
            AuthorId = book.AuthorId
        }).ToList();

        // Return the result
        return Ok(authorBookDTOs);
    }


}
