using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Analise
{
    public class Formatacao
    {
        public string Original { get; set; }
        public string Formatado { get; set; }
        public List<Filtro> Filtros { get; set; }

        public static Formatacao FormatarTexto(string texto)
        {
            var filtro = default(Filtro);
            var list = new List<Filtro>();
            string formatado = string.Empty;
            for (int i = 0; i < texto.Length; i++)
            {
                switch (texto[i])
                {
                    //case ' ':
                    //    if (texto[i + 1] == ' ')
                    //    {
                    //        filtro = IgnoraEspaco(ref i, texto);
                    //        list.Add(filtro);
                    //    }
                    //    break;
                    case '#':
                        filtro = IgnoraHashtag(ref i, texto);
                        list.Add(filtro);
                        break;
                    default:
                        char c = texto[i];
                        // 3 letras repetidas
                        if (!char.IsDigit(c) && c!='.' && texto.Length-i>3 && texto[i+1]==c && texto[i + 2] == c)
                        {
                            if (c == 'w' || c == 'W') filtro = IgnoraLink(ref i, texto);
                            else filtro = IgnoraRepetida(ref i, texto);
                            list.Add(filtro);
                        }
                        else if (texto.Length - i > 7 && texto[i] == 'h' && texto[i + 1] == 't' && texto[i + 2] == 't' && texto[i + 3] == 'p' &&
                            texto[i + 4] == ':' && texto[i + 5] == '/' && texto[i + 6] == '/') 
                        {
                            filtro = IgnoraLink(ref i, texto);
                            list.Add(filtro);
                        }
                        else
                        {
                            formatado += $"{texto[i]}";
                        }
                        break;
                }                
            }
            var format = new Formatacao();
            format.Filtros = list;
            format.Formatado = formatado;
            format.Original = texto;
            return format;
        }

        static Filtro IgnoraLink(ref int i, string texto)
        {
            Filtro filtro = new Filtro
            {
                Inicio = i,
                Tipo = TipoFiltro.Link,
            };
            for (int k = i; k < texto.Length; k++)
            {
                i = k;
                if (texto[i] == ' ') break;
            }
            filtro.Fim = i;
            i--;
            return filtro;
        }

        static Filtro IgnoraEspaco(ref int i, string texto)
        {
            var filtro = new Filtro
            {
                Inicio = i,
                Fim = i + 1,
                Tipo = TipoFiltro.CaracterBranco,
            };
            for (int k = i; k < texto.Length; k++)
            {
                i = k;
                if (texto[i] != ' ') break;
            }
            filtro.Fim = i;
            i--;
            return filtro;
        }

        static Filtro IgnoraHashtag(ref int i, string texto)
        {
            var filtro = new Filtro
            {
                Inicio = i,
                Tipo = TipoFiltro.Hashtag
            };
            for (int k = i; k < texto.Length; k++)
            {
                i = k;
                if (texto[i] == ' ') break;
            }
            filtro.Fim = i;
            return filtro;
        }

        static Filtro IgnoraRepetida(ref int i, string texto)
        {
            Filtro filtro = new Filtro
            {
                Inicio = i,
                Tipo = TipoFiltro.LetraRepetida,
            };
            char c = texto[i];
            for (int k = i; k < texto.Length; k++)
            {
                i = k;
                if (texto[i] != c) break;
            }
            filtro.Fim = i;
            i--;
            return filtro;
        }
    }
}
