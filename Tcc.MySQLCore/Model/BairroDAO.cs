using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;

namespace Tcc.MySQL.Model
{
    public class BairroDAO : Bairro
    {
        public long Id { get; set; }
        public long IdCidade { get; set; }

        public static void AdicionarBairro(Bairro bairro, long id_cidade)
        {
            try
            {
                var query = "INSERT INTO bairro (nome, id_cidade) VALUES (@Nome, @ID_CIDADE)";
                var cmd = new MySqlCommand(query, Conexao.Connection);
                cmd.Parameters.AddWithValue("@Nome", bairro.Nome);
                cmd.Parameters.AddWithValue("@ID_CIDADE", id_cidade);

                var r = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public static List<BairroDAO> BuscarTodosBairros(string cidade)
        {
            var bairros = new List<BairroDAO>();
            var query = "SELECT * FROM bairro as b, cidade as c WHERE b.id_cidade = c.id_cidade AND c.nome = @ID_CIDADE";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            cmd.Parameters.AddWithValue("@ID_CIDADE", cidade);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var bairro = new BairroDAO();
                bairro.Id = Convert.ToInt32(reader["id_bairro"]);
                bairro.IdCidade = Convert.ToInt32(reader["id_cidade"]);
                bairro.Nome = reader["nome"].ToString();
                bairro.NomeAlternativo = reader["nome_alternativo"].ToString();
                bairros.Add(bairro);
            }
            Conexao.Connection.Close();
            return bairros;
        }

        public static List<BairroDAO> BuscarTodosBairros(CidadeDAO cidade)
        {
            var bairros = new List<BairroDAO>();
            var query = "SELECT * FROM bairro WHERE id_cidade = @ID_CIDADE";
            var conn = new MySqlConnection(Conexao.ConnString);
            conn.Open();

            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID_CIDADE", cidade.Id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var bairro = new BairroDAO();
                bairro.Id = Convert.ToInt32(reader["id_bairro"]);
                bairro.IdCidade = Convert.ToInt32(reader["id_cidade"]);
                bairro.Nome = reader["nome"].ToString();
                bairro.NomeAlternativo = reader["nome_alternativo"].ToString();
                bairros.Add(bairro);
            }
            conn.Close();
            return bairros;
        }

        public static List<BairroDAO> BuscarBairrosSemLogradouros(CidadeDAO cidade)
        {
            var bairros = new List<BairroDAO>();
            var query = "SELECT * FROM bairro as b WHERE not exists (select * from logradouro as l where l.id_bairro = b.id_bairro) AND id_cidade = @ID_CIDADE;";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            cmd.Parameters.AddWithValue("@ID_CIDADE", cidade.Id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var bairro = new BairroDAO();
                bairro.Id = Convert.ToInt32(reader["id_bairro"]);
                bairro.IdCidade = Convert.ToInt32(reader["id_cidade"]);
                bairro.Nome = reader["nome"].ToString();
                bairro.NomeAlternativo = reader["nome_alternativo"].ToString();
                bairros.Add(bairro);
            }
            Conexao.Connection.Close();
            return bairros;
        }
    }
}
