using System;
using CosmosMongoDBApi.Models;
using CosmosMongoDBApi.Models.DbContext;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CosmosMongoDBApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    public readonly MongoDbContext _context;

    public BooksController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _context.Books.Find(book => true).ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(string id)
    {
        var book = await _context.Books.Find(book => book.Id == id).FirstOrDefaultAsync();
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(Book book)
    {
        await _context.Books.InsertOneAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(string id, Book updatedBook)
    {
        var bookToUpdate = await _context.Books.Find(book => book.Id == id).FirstOrDefaultAsync();
        if (bookToUpdate is null)
        {
            return NotFound();
        }

        updatedBook.Id = bookToUpdate.Id;
        await _context.Books.ReplaceOneAsync(book => book.Id == id, updatedBook);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(string id)
    {
        var result = await _context.Books.DeleteOneAsync(book => book.Id == id);
        if (result.IsAcknowledged && result.DeletedCount > 0)
        {
            return NoContent();
        }
        return NotFound();
    }
}
