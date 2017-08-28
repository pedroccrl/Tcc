using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;

namespace Tcc.MySQL.Model
{
    public class LogradouroDAO : Logradouro
    {
        public long Id { get; set; }
        public long IdBairro { get; set; }
        public long IdCidade { get; set; }

        public static void AdicionarLogradouro(Logradouro log, long id_cidade, long id_bairro)
        {
            try
            {
                var query = "INSERT INTO logradouro (nome, cep, id_cidade, id_bairro) VALUES (@Nome, @Cep, @ID_CIDADE, @ID_BAIRRO)";
                var cmd = new MySqlCommand(query, Conexao.Connection);
                cmd.Parameters.AddWithValue("@Nome", log.Nome);
                cmd.Parameters.AddWithValue("@Cep", log.Cep);
                cmd.Parameters.AddWithValue("@ID_CIDADE", id_cidade);
                cmd.Parameters.AddWithValue("@ID_BAIRRO", id_bairro);

                var r = cmd.ExecuteNonQuery();
                Console.WriteLine($"{log.Nome} - {log.Cep} adicionado ao bd");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{log.Nome} não adicionado ao bd. ERRO: {e.Message}");
            }

        }

        public static List<LogradouroDAO> BuscarTodosLogradouros(string cidade)
        {
            var logradouros = new List<LogradouroDAO>();
            var query = "SELECT * FROM logradouro as l, cidade as c WHERE l.id_cidade = c.id_cidade AND c.nome = @ID_CIDADE";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            cmd.Parameters.AddWithValue("@ID_CIDADE", cidade);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var logradouro = new LogradouroDAO();
                logradouro.Id = Convert.ToInt32(reader["id_logradouro"]);
                logradouro.IdCidade = Convert.ToInt32(reader["id_cidade"]);
                logradouro.IdBairro = Convert.ToInt32(reader["id_bairro"]);
                logradouro.Nome = reader["nome"].ToString();
                logradouro.Cep = reader["cep"].ToString();
                logradouros.Add(logradouro);
            }
            Conexao.Connection.Close();
            return logradouros;
        }
    }
}
