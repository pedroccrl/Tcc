using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Core.Mongo;

namespace Tcc.Core.Models
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }

        public static IMongoCollection<T> GetCollection<T>()
        {
            var db = MongoConnection.Database;
            var typeName = typeof(T).Name;

            return db.GetCollection<T>(typeName);
        }
    }
}
