using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Analise
{
    public class Entidade
    {
        public string Nome { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
        public TipoEntidade Tipo { get; set; }
        public int FimFrase { get; set; }
        static string[] locais = "rua,ruas,bairro,logradouro,avenida,travessa,rodovia,praça,alameda,beco,travessa,estrada,orla,pracinha"
                                 .Split(',');
        public static List<Entidade> ReconhecerEntidades(string texto)
        {
            var list = new List<Entidade>();
            texto = StringAcento.RemoverAcento(texto.ToLower());
            var original = texto;
            bool tem = false;
            // procura local no texto
            for (int i = 0; i < original.Length; i++)
            {
                foreach (var lcl in locais)
                {
                    var local = $" {StringAcento.RemoverAcento(lcl)} ";
                    int index = texto.IndexOf(local);
                    if (index != -1)
                    {
                        index++;
                        var entidade = new Entidade
                        {
                            Inicio = i + index,
                            Fim = i + index + lcl.Length,
                            Tipo = TipoEntidade.Local,
                            Nome = lcl
                        };
                        list.Add(entidade);
                        tem = true;
                        texto = texto.Substring(index + lcl.Length);
                        i = index + lcl.Length;
                    }
                }
                if (!tem) break;
            }
            // procura onde a frase que fala do local termina
            for (int i = 0; i < list.Count; i++)
            {
                var ent = list[i];
                var ent_prox = default(Entidade);
                if (i + 1 < list.Count) ent_prox = list[i + 1];

                var sub = original.Substring(ent.Fim);
                if (ent_prox != null)
                {
                    sub = sub.Substring(0, ent_prox.Inicio - ent.Fim);
                }
                for (int j = 0; j < sub.Length; j++)
                {
                    var c = sub[j];
                    switch (c)
                    {
                        case '.':
                        case '?':
                        case '!':
                        case ':':
                            ent.FimFrase = ent.Fim + j;
                            j = sub.Length;
                            break;
                        default:
                            break;
                    }
                }
            }


            return list;
        }
    }

    public enum TipoEntidade
    {
        Local,
        Localidade,
    }
}
