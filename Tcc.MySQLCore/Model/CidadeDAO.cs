using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model;

namespace Tcc.MySQL.Model
{
    public class CidadeDAO : Cidade
    {
        public long Id { get; set; }
        public List<BairroDAO> BairrosDAO { get; set; }

        public static void AdicionarCidade(Cidade cid, bool addBairros = true)
        {
            try
            {
                var query = "INSERT INTO cidade (nome, estado) VALUES (@Nome, @UF)";
                var cmd = new MySqlCommand(query, Conexao.Connection);
                cmd.Parameters.AddWithValue("@Nome", cid.Nome.Trim().ToLower());
                cmd.Parameters.AddWithValue("@UF", cid.UF.ToUpper());

                var r = cmd.ExecuteNonQuery();
                var id = cmd.LastInsertedId;

                Console.WriteLine("Cidade adicionada.");

                if (addBairros == true && cid.Bairros != null)
                {
                    foreach (var b in cid.Bairros)
                    {
                        BairroDAO.AdicionarBairro(b, id);
                        Console.WriteLine($"Bairro {b.Nome} adicionado.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }

        public static CidadeDAO BuscarCidade(string nome)
        {
            var cidade = new CidadeDAO();
            var query = "SELECT * FROM cidade WHERE nome = @Nome";

            var conn = new MySqlConnection(Conexao.ConnString);
            conn.Open();

            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nome", nome.Trim().ToLower());
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cidade.Nome = reader["nome"].ToString();
                cidade.UF = reader["estado"].ToString();
                cidade.Id = Convert.ToInt32(reader["id_cidade"]);
            }
            conn.Close();
            cidade.BairrosDAO = BairroDAO.BuscarTodosBairros(cidade);
            return cidade;
        }

        public static List<CidadeDAO> BuscarCidades()
        {
            
            var query = "SELECT * FROM cidade";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            var reader = cmd.ExecuteReader();
            var list = new List<CidadeDAO>();
            while (reader.Read())
            {
                var cidade = new CidadeDAO();
                cidade.Nome = reader["nome"].ToString();
                cidade.UF = reader["estado"].ToString();
                cidade.Id = Convert.ToInt32(reader["id_cidade"]);
                list.Add(cidade);
            }
            Conexao.Connection.Close();
            return list;
        }

        public static async Task<List<CidadeDAO>> BuscarCidadesAsync()
        {
            var cidades = default(List<CidadeDAO>);
            await Task.Run(() =>
            {
                cidades = BuscarCidades();
            });
            return cidades;
        }

        public static CidadeDAO BuscarCidadeSemLogradouro(string nome)
        {
            var cidade = new CidadeDAO();
            var query = "SELECT * FROM cidade WHERE nome = @Nome";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            cmd.Parameters.AddWithValue("@Nome", nome.Trim().ToLower());
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cidade.Nome = reader["nome"].ToString();
                cidade.UF = reader["estado"].ToString();
                cidade.Id = Convert.ToInt32(reader["id_cidade"]);
            }
            Conexao.Connection.Close();
            cidade.BairrosDAO = BairroDAO.BuscarBairrosSemLogradouros(cidade);
            return cidade;
        }
    }
}
