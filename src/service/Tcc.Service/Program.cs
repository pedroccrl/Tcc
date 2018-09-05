using System;
using Tcc.Core.Mongo;

namespace Tcc.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoSettings = default(MongoSettings);
            if (args.Length > 0 && args[0] == "prod")
            {
                mongoSettings = new MongoSettings
                {
                    ConnectionString = "mongodb://admin:812110ab@ds125628.mlab.com:25628/tcc",
                    Database = "tcc"
                };
            }
            else
            {
                mongoSettings = new MongoSettings
                {
                    ConnectionString = "mongodb://localhost:27017",
                    Database = "tcc"
                };
            }

            MongoConnection.Init(mongoSettings);

            Tasks.PlaceFinderTask.BuscaCidadesCorreios().Wait();
        }
    }
}
