using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.MySQL;
using Tcc.Nucleo.Model.Facebook;

namespace Tcc.MySQL.Model
{
    public class PaginaFacebookDAO : Page
    {
        public long Id { get; set; }
        public long IdCidade { get; set; }
        public static bool AdicionarPagina(Page page, string cidade)
        {
            try
            {
                var query = "INSERT INTO pagina (id_redesocial, link, nome, tipo_redesocial, id_cidade) select @ID_REDESOCIAL, @Link, @Nome, 'Facebook', id_cidade from cidade where nome = @Cidade";
                var cmd = new MySqlCommand(query, Conexao.Connection);
                cmd.Parameters.AddWithValue("@ID_REDESOCIAL", page.id);
                cmd.Parameters.AddWithValue("@Link", page.link);
                cmd.Parameters.AddWithValue("@Nome", page.name);
                cmd.Parameters.AddWithValue("@Cidade", cidade);
                
                var r = cmd.ExecuteReader();
                var id = cmd.LastInsertedId;

                Console.WriteLine("Pagina adicionada.");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        } 

        public static List<PaginaFacebookDAO> ObterTodasPaginas()
        {
            var query = "SELECT * FROM pagina";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            var reader = cmd.ExecuteReader();
            var paginas = new List<PaginaFacebookDAO>();
            while (reader.Read())
            {
                var page = new PaginaFacebookDAO();
                page.name = reader["nome"].ToString();
                page.id = reader["id_redesocial"].ToString();
                page.Id = Convert.ToInt32(reader["id_pagina"]);
                page.IdCidade = Convert.ToInt32(reader["id_cidade"]);
                paginas.Add(page);
            }
            return paginas;
        }

        public static List<PaginaFacebookDAO> ObterPaginasCidade(string cidade)
        {
            var query = "SELECT * FROM pagina as p, cidade as c WHERE p.id_cidade = c.id_cidade AND c.nome = @Cidade";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            cmd.Parameters.AddWithValue("@Cidade", cidade);
            var reader = cmd.ExecuteReader();
            var paginas = new List<PaginaFacebookDAO>();
            while (reader.Read())
            {
                var page = new PaginaFacebookDAO();
                page.name = reader["nome"].ToString();
                page.id = reader["id_redesocial"].ToString();
                page.Id = Convert.ToInt32(reader["id_pagina"]);
                page.IdCidade = Convert.ToInt32(reader["id_cidade"]);
                paginas.Add(page);
            }
            Conexao.Connection.Close();
            return paginas;
        }

        public static async Task<List<PaginaFacebookDAO>> ObterPaginasCidadeAsync(string cidade)
        {
            var paginas = default(List<PaginaFacebookDAO>);
            await Task.Run(() =>
            {
                paginas = ObterPaginasCidade(cidade);
            });
            return paginas;
        }
    }
}
