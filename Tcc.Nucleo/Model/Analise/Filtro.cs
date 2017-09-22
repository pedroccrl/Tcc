using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Analise
{
    public class Filtro
    {
        public TipoFiltro Tipo { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
    }

    public enum TipoFiltro
    {
        Link,
        /// <summary>
        /// Mais que 3 letras
        /// </summary>
        LetraRepetida,
        CaracterBranco,
        Hashtag,
    }
}
