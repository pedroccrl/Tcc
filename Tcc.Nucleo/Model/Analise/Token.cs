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

        static string[] preposicoes = "da,de,do,na,no,em"
                                      .Split(',');

        public static List<Token> Tokenize(string texto)
        {
            var list = new List<Token>();
            texto = StringAcento.RemoverAcento(texto.ToLower());
            var original = texto;
            bool temPrep = false;
            for (int i = 0; i < original.Length; i++)
            {
                foreach (var prep in preposicoes)
                {
                    var preposicao = $" {prep} ";
                    int index = texto.IndexOf(preposicao);
                    if (index != -1)
                    {
                        var token = new Token
                        {
                            Inicio = index,
                            Fim = index + prep.Length,
                            Tipo = TipoToken.Preposicao
                        };
                        list.Add(token);
                        temPrep = true;
                        texto = texto.Substring(index + prep.Length);
                        i = index + prep.Length;
                    }
                }
                if (!temPrep) break;
            }
            return list;
        }
    }

    public enum TipoToken
    {
        Local = -2,
        Pessoa = -1,
        Desconhecido,
        Preposicao,
    }
}
