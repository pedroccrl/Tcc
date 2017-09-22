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
                    default:
                        break;
                }
            }
        }
    }
}
