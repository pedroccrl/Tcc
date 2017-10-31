using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.MySQL.Model
{
    public class PalavraDAO
    {
        public string Palavra { get; set; }
        public string Normalizada { get; set; }

        public static List<PalavraDAO> BuscarTodasPalavras()
        {
            var query = "SELECT * FROM word";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            var reader = cmd.ExecuteReader();
            var list = new List<PalavraDAO>();
            while (reader.Read())
            {
                var p = new PalavraDAO
                {
                    Palavra = reader["word"].ToString(),
                    Normalizada = reader["normalized"].ToString()
                };
                list.Add(p);
            }
            return list;
        }
    }
}
