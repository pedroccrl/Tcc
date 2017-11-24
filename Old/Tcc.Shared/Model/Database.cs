using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;
using Tcc.Shared.Model.Crawler;

namespace Tcc.Shared.Model
{
    public static class Database
    {
        public static void GroupBairrosPorLogradouro()
        {
            var mongo = new MongoClient("mongodb://localhost:27017");
            var db = mongo.GetDatabase("logradouros");
            var coll = db.GetCollection<Logradouro>("rio das ostras");

            //var bairros = coll.Find(new BsonDocument()).ToList().FindAll(l=>l.cidade.Contains("Rio das Ostras")).GroupBy(l=>l.bairro);

            //foreach (var item in bairros)
            //{
            //    Console.WriteLine(item.Key);
            //    Correios.BuscaLogradouroPorBairro(item.Key, "rio das ostras", "rj");
            //}
        }
    }
}
