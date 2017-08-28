using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.MySQL.Model;
using Tcc.Nucleo.Model.Analise;
using static System.Console;
namespace Tcc.Comando.Model
{
    public class Analise
    {
        public static void AnalisarBairrosEmPosts()
        {
            WriteLine("Nome da cidade: ");
            var cidade = ReadLine();
            var bairros = BairroDAO.BuscarTodosBairros(cidade);
            var posts = PostFacebookDAO.BuscarTodosPosts();
            var list = new List<Token>();
            var sw = new Stopwatch();
            sw.Start();
            foreach (var post in posts)
            {
                foreach (var bairro in bairros)
                {
                    var token = Analisador.GetToken(post.message, bairro.Nome, TipoToken.Local);
                    if (token != null)
                    {
                        list.Add(token);
                    }
                }
            }
            sw.Stop();
        }

        public static void AnalisarLogradourosEmPosts()
        {
            WriteLine("Nome da cidade: ");
            var cidade = ReadLine();
            var logradouros = LogradouroDAO.BuscarTodosLogradouros(cidade);
            var posts = PostFacebookDAO.BuscarTodosPosts();
            var list = new List<Token>();
            var sw = new Stopwatch();
            sw.Start();
            foreach (var post in posts)
            {
                foreach (var logradouro in logradouros)
                {
                    var token = Analisador.GetToken(post.message, logradouro.Nome, TipoToken.Local);
                    if (token != null)
                    {
                        list.Add(token);
                    }
                }
            }
            sw.Stop();
        }
    }
}
