using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Models
{
    [BsonIgnoreExtraElements]
    public class Config
    {
        public string SiteTitulo { get; set; }
    }
}
    