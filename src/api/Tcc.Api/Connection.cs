using MongoDB.Driver;

namespace Tcc.Api
{
    public static class Connection
    {
        static MongoClient Client;
        public static IMongoDatabase Database;

        static Connection()
        {
            Client = new MongoClient("mongodb://admin:812110ab@ds125628.mlab.com:25628/tcc");
            Database = Client.GetDatabase("tcc");
        }
    }
}
