using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Analise
{
    public class Relacao
    {
        public long IdCidade { get; set; } = -1;
        public long IdBairro { get; set; } = -1;
        public long IdLogradouro { get; set; } = -1;
        public long IdPagina { get; set; } = -1;
        public long IdPost { get; set; } = -1;
        public long IdComentario { get; set; } = -1;

        public string Data = $"{DateTime.Now:dd/MM/yyyy}";
    }
}
