using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Analise
{
    public class Token
    {
        public string Palavra { get; set; }
        public TipoToken Tipo { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
    }

    public enum TipoToken
    {
        Local,
        Pessoa,

    }
}
