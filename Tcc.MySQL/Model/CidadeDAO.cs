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
            cidade.BairrosDAO = BairroDAO.BuscarTodosBairros(cidade);
            return cidade;
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
