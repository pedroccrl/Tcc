using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Models
{
    public class Logradouro : GeoEntity
    {
        public string Nome { get; set; }
        public string Cep { get; set; }
        public ObjectId BairroId { get; set; }
        public ObjectId CidadeId { get; set; }
    }
}
