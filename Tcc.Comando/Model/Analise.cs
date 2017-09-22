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
            var relacoes = new List<Relacao>();
            var sw = new Stopwatch();
            sw.Start();
            foreach (var post in posts)
            {
                var formatado = Formatacao.FormatarTexto(post.message);
                foreach (var bairro in bairros)
                {
                    var token = Analisador.GetToken(post.message, bairro.Nome, TipoToken.Local);
                    if (token != null)
                    {
                        list.Add(token);
                        relacoes.Add(new Relacao
                        {
                            IdBairro = bairro.Id,
                            IdCidade = bairro.IdCidade,
                            IdPost = post.Id,
                            IdPagina = post.IdPagina,
                        });
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
            var relacoes = new List<Relacao>();
            var sw = new Stopwatch();
            sw.Start();
            foreach (var post in posts)
            {
                var formatado = Formatacao.FormatarTexto(post.message);
                foreach (var logradouro in logradouros)
                {
                    var token = Analisador.GetToken(post.message, logradouro.Nome, TipoToken.Local);
                    if (token != null)
                    {
                        list.Add(token);
                        relacoes.Add(new Relacao
                        {
                            IdLogradouro = logradouro.Id,
                            IdCidade = logradouro.IdCidade,
                            IdPost = post.Id,
                            IdPagina = post.IdPagina,
                        });
                    }
                }
            }
            sw.Stop();
        }
    }
}
