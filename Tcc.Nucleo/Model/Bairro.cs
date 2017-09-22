using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model
{
    public class Bairro
    {
        public string Nome { get; set; }
        public string NomeAlternativo { get; set; }
        public List<Logradouro> Logradouros { get; set; }
    }
}
