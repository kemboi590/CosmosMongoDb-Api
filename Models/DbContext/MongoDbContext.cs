using MongoDB.Driver;

namespace CosmosMongoDBApi.Models.DbContext;
// public interface IMongoDbContext
// {
//     IMongoCollection<Book> Books { get; }
//     IMongoCollection<Author> Authors { get; }
// }


public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration consfiguration)
    {
        var client = new MongoClient(consfiguration.GetConnectionString("MongoDB"));
        _database = client.GetDatabase(consfiguration["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books");
    public IMongoCollection<Author> Authors => _database.GetCollection<Author>("Authors");

}
