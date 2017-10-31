using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.MySQL.Model
{
    public class LogradouroDoc : LogradouroDAO
    {
        public BairroDoc Bairro { get; set; }
    }

    public class BairroDoc
    {
        public string Nome { get; set; }
        public string NomeAlternativo { get; set; }
        public string Cidade { get; set; }
    }
}
