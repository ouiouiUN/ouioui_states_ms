namespace States_ms.Contexts
{
    using States_ms.Models;
    using States_ms.Configurations;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using System;
    public class StateContext: IStateContext
    {
        private readonly IMongoDatabase _db;
        public StateContext(MongoDBConfig config)
        {
            Console.WriteLine(config.ConnectionString);
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
            Console.WriteLine(_db);
            var filter = new BsonDocument("name", "states");
            var options = new ListCollectionNamesOptions { Filter = filter };
            Console.WriteLine(_db.ListCollectionNames(options).Any());
            
        }
        public IMongoCollection<State> States => _db.GetCollection<State>("states" );

    }
}