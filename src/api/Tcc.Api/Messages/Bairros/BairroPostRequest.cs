using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Messages.Bairros
{
    public class BairroPostRequest
    {
        public string CidadeId { get; set; }
        public string Nome { get; set; }
        public string NomeAlternativo { get; set; }
    }
}
