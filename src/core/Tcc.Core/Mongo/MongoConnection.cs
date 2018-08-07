using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Mongo
{
    public class MongoConnection
    {
        public static IMongoDatabase Database { get; set; }

        public static void Init(MongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.Database);
        }
    }
}
