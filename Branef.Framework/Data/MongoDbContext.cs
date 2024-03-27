using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Branef.Framework.Data
{
    public class MongoDbContext
    {
        public readonly IMongoDatabase Database = null!;

        public MongoDbContext(string connectionString, string database)
        {
            var mongoClient = new MongoClient(connectionString);
            this.Database = mongoClient.GetDatabase(database);
        }

    }
}
