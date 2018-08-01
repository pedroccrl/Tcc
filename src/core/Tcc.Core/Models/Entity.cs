using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Core.Models
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }
    }
}
