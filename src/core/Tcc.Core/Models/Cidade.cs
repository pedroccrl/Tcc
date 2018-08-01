using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Models
{
    public class Cidade : GeoEntity
    {
        public string Nome { get; set; }
        public string UF { get; set; }

        [BsonIgnore]
        public List<Bairro> Bairros { get; set; }
    }
}
