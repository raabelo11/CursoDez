using MongoDB.Driver;

namespace CursoDez.Infrastructure.Context
{
    public class CursoDezContextMongoDB
    {
        private readonly IMongoDatabase _database;

        public CursoDezContextMongoDB(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
    }
}
