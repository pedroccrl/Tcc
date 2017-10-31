using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace Tcc.Comando
{
    public static class MenuSettings
    {
        public static void Menu()
        {
            bool loop = true;
            while (loop)
            {
                WriteLine("1 - Inserir comentarios do MySQL no Mongo");
                WriteLine("2 - Inserir logradouros no mongo");
                WriteLine("3 - Salvar palavras do dicionario no Mongo");
                WriteLine("4 - Salvar comentarios com localidades no Mongo");
                WriteLine("5 - Salvar bairros no Mongo");
                WriteLine("5 - Salvar logradouros doc no Mongo");
                WriteLine("0 - Sair");
                var opcao = ReadLine();
                switch (opcao)
                {
                    case "0":
                        loop = false;
                        break;
                    case "1":
                        WriteLine("Buscando comentarios no MySQL...");
                        var comentarios = MySQL.Model.ComentarioFacebookDAO.BuscarTodosComentarios();
                        WriteLine($"{comentarios.Count} encontrados");
                        MongoConector.AcessoDados.SalvarComentarios(comentarios, "comentarios_original");
                        WriteLine("Finalizado");
                        break;

                    case "2":
                        WriteLine("Digite o nome da cidade:");
                        var cidade = ReadLine();
                        var logradouros = MySQL.Model.LogradouroDAO.BuscarTodosLogradouros(cidade);
                        MongoConector.AcessoDados.SalvarLogradourosSplit(logradouros);
                        break;
                    case "3":
                        MongoConector.AcessoDados.SalvarPalavrasMongo();
                        WriteLine("Pronto!");
                        break;
                    case "4":
                        WriteLine("Digite uma localidade:");
                        var l = ReadLine();
                        var coments = MySQL.Model.ComentarioFacebookDAO.BuscarTodosComentariosComLocalidade(l);
                        MongoConector.AcessoDados.SalvarComentarios(coments, $"{l}s");
                        break;
                    case "5":
                        WriteLine("Digite a cidade:");
                        var c = ReadLine();
                        var bairros = MySQL.Model.BairroDAO.BuscarTodosBairros(c);
                        MongoConector.AcessoDados.SalvarBairros(bairros);
                        break;
                    case "6":
                        WriteLine("Digite a cidade:");
                        var cid = ReadLine();
                        var ls = MySQL.Model.LogradouroDAO.BuscarTodosLogradourosDoc(cid);
                        MongoConector.AcessoDados.SalverLogradourosDoc(ls);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
