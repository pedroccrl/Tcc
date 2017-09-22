using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Analise
{
    public class Arvore
    {
        public List<Token> Tokens { get; set; }
        public List<Entidade> Entidades { get; set; }
        public Formatacao Formatacao { get; set; }
    }
}
