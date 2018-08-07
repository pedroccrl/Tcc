using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Messages.Bairros
{
    public class BairrosPostRequest
    {
        public string CidadeId { get; set; }
        public string[] Bairros { get; set; }
    }
}
