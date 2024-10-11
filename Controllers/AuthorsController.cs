using System;
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

    public AuthorsController(MongoDbContext context)
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

    // [HttpGet("author-books")]
    // public async Task<IActionResult> GetAuthorBookInfo()
    // {
    //     var aggregation = _context.Books.Aggregate()
    //         .Lookup<Book, Author, AuthorBookInfo>(
    //             _context.Authors,
    //             book => book.AuthorId,
    //             author => author.Id,
    //             result => result.AuthorInfo
    //         )
    //         .Project(authorBook => new AuthorBookInfo
    //         {
    //             AuthorName = authorBook.AuthorInfo.FirstOrDefault().Name,
    //             AuthorBio = authorBook.AuthorInfo.FirstOrDefault().Bio,
    //             BookTitle = authorBook.Title,
    //             PublishedYear = authorBook.PublishedYear
    //         })
    //         .ToListAsync();

    //     var result = await aggregation;

    //     return Ok(result);
    // }

}
