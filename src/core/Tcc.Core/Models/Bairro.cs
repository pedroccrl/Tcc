using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Models
{
    public class Bairro : GeoEntity
    {
        public string Nome { get; set; }
        public string NomeAlternativo { get; set; }
        public ObjectId CidadeId { get; set; }

        [BsonIgnore]
        public List<Logradouro> Logradouros { get; set; }

        public bool Completo { get; set; }
    }
}
