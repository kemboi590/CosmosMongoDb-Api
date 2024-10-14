# Azure Cosmos DB for Mongo DB connecting to ASP .NET core web Api and performing operations.

- Remember to set appsettings.json as expected with correct details

```
{
  "ConnectionStrings": {
    "MongoDB": "mongodb+srv://<admin>:<Password>@cosmos-mogndb.mongocluster.cosmos.azure.com/?tls=true&authMechanism=SCRAM-SHA-256&retrywrites=false&maxIdleTimeMS=120000"
  },
  "MongoDB": {
    "DatabaseName": "LibraryDB"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

- dotnet watch run --launch-profile https
