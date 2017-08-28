using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model.Facebook;
using static Tcc.Nucleo.Model.StringAcento;

namespace Tcc.Nucleo.Model.Analise
{
    public class Analisador
    {
        static string tipo_locais = "rua,ruas,bairro,logradouro,avenida,travessa,rodovia,praça,alameda,beco,travessa,estrada,cidade,orla";

        public static Token GetToken(string texto, string comparar, TipoToken tokenEsperado)
        {
            var textoF = RemoverAcento(texto.ToLower().Replace(" ", ""));
            var compararF = RemoverAcento(comparar.ToLower().Replace(" ", ""));

            int ini = 0, fim = 0, p = 0;
            bool achou = false;

            for (int i = 0; i < textoF.Length; i++)
            {
                ini = i;
                p = i;
                for (int j = 0; j < compararF.Length; j++)
                {
                    if (textoF[p] == compararF[j])
                    {
                        achou = true;
                        p++;
                        if (p > textoF.Length - 1)
                        {
                            achou = false;
                            break;
                        }
                        continue;
                    }
                    else
                    {
                        achou = false;
                        break;
                    }
                }
                if (achou)
                {
                    fim = p;
                    var token = new Token
                    {
                        Inicio = ini,
                        Fim = fim,
                        Palavra = comparar,
                        Tipo = tokenEsperado
                    };
                    return token;
                }
                fim = i;
            }
            return null;
        }

        public static int ContaLocalNoPost(List<Post> posts, IStatus status = null)
        {
            int total = 0;
            int encontrados = 0;
            var locais = tipo_locais.Split(',');
            foreach (var post in posts)
            {
                var titulo = post.message.ToLower();
                foreach (var local in locais)
                {
                    if (titulo.Contains(local))
                    {
                        encontrados++;
                        status?.Escrever($"{encontrados} encontrados de {total}/{posts.Count}");
                        break;
                    }
                }
                total++;
            }
            return total;
        }

        public static int ContaLocalNosComentarios(List<Comment> comments, IStatus status = null)
        {
            int total = 0;
            int encontrados = 0;
            var locais = tipo_locais.Split(',');
            foreach (var item in comments)
            {
                var texto = item.message.ToLower();
                foreach (var local in locais)
                {
                    if (texto.Contains(local))
                    {
                        encontrados++;
                        status?.Escrever($"{encontrados} encontrados de {total}/{comments.Count}");
                        break;
                    }
                }
                total++;
            }
            return total;
        }
    }
}
