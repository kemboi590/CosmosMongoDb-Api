using MongoDB.Driver;

namespace CosmosMongoDBApi.Models.DbContext;

public class MongoDbContext
{
    private readonly IMongoDatabase _database; // used to interact with the database

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB")); //connect to the database
        _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]); // get the database
    }

    public IMongoCollection<Book> Books => _database.GetCollection<Book>("Books"); 
    public IMongoCollection<Author> Authors => _database.GetCollection<Author>("Authors");

}
