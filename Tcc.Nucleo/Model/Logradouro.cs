using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model
{
    public class Logradouro
    {
        /// <summary>
        /// Impedir que o algoritmo identifique este logradouro
        /// </summary>
        const string NAO_RECONHECER = "/N.A.O_D.E.V.E_S.E.R_U.S.A.D.O./";
        public string Nome { get; set; }
        public string Cep { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        /// <summary>
        /// Rua, travessa, praça, avenida, etc...
        /// </summary>
        public string Tipo
        {
            get
            {
                var tipo = Nome.Split(' ')[0];
                return tipo;
            }
        }

        /// <summary>
        /// Sem o tipo e numeração
        /// </summary>
        public string NomeSimples
        {
            get
            {
                var nome = "";
                if (Nome.Contains(" - "))
                {
                    var nomes = Nome.Split('-');
                    nome = nomes[0].Replace(Tipo, "").Trim();
                }
                else
                {
                    nome = Nome.Replace(Tipo, "").Trim();
                }
                
                if (nome.Length < 3) return NAO_RECONHECER;
                else if (nome == "um" || nome == "dois" || nome == "tres" || nome == "quatro" || nome == "cinco" || nome == "seis" || nome == "sete" || nome == "oito" || nome == "nove" || nome == "dez" || nome == "onze")
                    return NAO_RECONHECER;
                else return nome;
            }
        }
    }
}
