using CosmosMongoDBApi.Models.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MongoDbContext>(); // add the db context to the services, so it can be injected


builder.Services.AddControllers()
    .AddJsonOptions(
        options =>options.JsonSerializerOptions.PropertyNamingPolicy = null); // to avoid camel case
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//summary of the above code:builder.Services.AddSingleton<MongoDbContext>();
/*
- The AddSingleton method registers the MongoDbContext class with the dependency injection container.
- dependency injection is a technique in which an object receives other objects that it depends on.
- The MongoDbContext class is registered as a singleton service, which means that only one instance of the class is created and shared across the application.
- This ensures that the same instance of the MongoDbContext class is used throughout the application, which is important for maintaining a connection to the database and managing data consistency.
- The MongoDbContext class is used to interact with the MongoDB database and provides access to the collections of books and authors.
- By registering the MongoDbContext class with the dependency injection container, it can be injected into other classes that require access to the database, such as controllers and services.
- This allows those classes to interact with the database without having to manage the connection themselves, making the code more modular and easier to maintain.
- The AddSingleton method is part of the IServiceCollection interface, which is used to register services with the dependency injection container in ASP.NET Core.
- The IServiceCollection interface provides a fluent API for registering services, such as AddSingleton, AddTransient, and AddScoped, which are used to specify the lifetime of the service.

*/
