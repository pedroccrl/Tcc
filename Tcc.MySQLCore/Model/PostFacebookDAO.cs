using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model.Facebook;

namespace Tcc.MySQL.Model
{
    public class PostFacebookDAO : Post
    {
        public long Id { get; set; }
        public long IdPagina { get; set; }
        public long IdCidade { get; set; }

        public static bool AdicionarPost(Post post, long idCidade, long idPagina)
        {
            try
            {
                Conexao.Connection.Close();
                var query = "INSERT INTO post (id_pagina, id_cidade, data, titulo, id_redesocial, link) VALUES (@ID_PAGINA, @ID_CIDADE, @Data, @Titulo, @ID_REDESOCIAL, @Link)";
                var cmd = new MySqlCommand(query, Conexao.Connection);
                cmd.Parameters.AddWithValue("@ID_PAGINA", idPagina);
                cmd.Parameters.AddWithValue("@ID_CIDADE", idCidade);
                var date = DateTime.ParseExact(post.created_time, "yyyy-MM-ddTHH:mm:ss+0000", null);
                var datetimeMysql = $"{date:yyyy-MM-dd HH:mm:ss}";
                cmd.Parameters.AddWithValue("@Data", datetimeMysql);
                cmd.Parameters.AddWithValue("@Titulo", post.message);
                cmd.Parameters.AddWithValue("@ID_REDESOCIAL", post.id);
                cmd.Parameters.AddWithValue("@Link", post.link);
                var r = cmd.ExecuteNonQuery();
                
                Conexao.Connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static long AdicionarPostId(Post post, long idCidade, long idPagina)
        {
            try
            {
                Conexao.Connection.Close();
                var query = "INSERT INTO post (id_pagina, id_cidade, data, titulo, id_redesocial, link) VALUES (@ID_PAGINA, @ID_CIDADE, @Data, @Titulo, @ID_REDESOCIAL, @Link)";
                var cmd = new MySqlCommand(query, Conexao.Connection);
                cmd.Parameters.AddWithValue("@ID_PAGINA", idPagina);
                cmd.Parameters.AddWithValue("@ID_CIDADE", idCidade);
                var date = DateTime.ParseExact(post.created_time, "yyyy-MM-ddTHH:mm:ss+0000", null);
                var datetimeMysql = $"{date:yyyy-MM-dd HH:mm:ss}";
                cmd.Parameters.AddWithValue("@Data", datetimeMysql);
                cmd.Parameters.AddWithValue("@Titulo", post.message);
                cmd.Parameters.AddWithValue("@ID_REDESOCIAL", post.id);
                cmd.Parameters.AddWithValue("@Link", post.link);
                var r = cmd.ExecuteNonQuery();

                Conexao.Connection.Close();
                return cmd.LastInsertedId;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static async Task<long> AdicionarPostIdAsync(Post post, long idCidade, long idPagina)
        {
            var id = -1;
            await Task.Run(() =>
            {
                AdicionarPostId(post, idCidade, idPagina);
            });
            return id;
        }

        public static List<PostFacebookDAO> BuscarTodosPosts()
        {
            var query = "SELECT * FROM post";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            var reader = cmd.ExecuteReader();
            List<PostFacebookDAO> list = new List<PostFacebookDAO>();
            while (reader.Read())
            {
                var data = reader["data"].ToString();
                var post = new PostFacebookDAO
                {
                    Id = int.Parse(reader["id_post"].ToString()),
                    IdPagina = int.Parse(reader["id_pagina"].ToString()),
                    IdCidade = int.Parse(reader["id_cidade"].ToString()),
                    id = reader["id_redesocial"].ToString(),
                    link = reader["link"].ToString(),
                    Data = DateTime.ParseExact(data, "dd/MM/yyyy HH:mm:ss", null),
                    message = reader["titulo"].ToString(),
                };
                list.Add(post);
            }
            Conexao.Connection.Close();
            return list;
        }

        public static List<PostFacebookDAO> BuscarPostsPagina(long idPagina, int ultimosDias = 0)
        {
            Conexao.Connection.Close();
            var query = $"SELECT * FROM post WHERE id_pagina = {idPagina}";
            if (ultimosDias > 0) query += $" and data > now() - interval {ultimosDias} day";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            var reader = cmd.ExecuteReader();
            List<PostFacebookDAO> list = new List<PostFacebookDAO>();
            while (reader.Read())
            {
                var data = reader["data"].ToString();
                var post = new PostFacebookDAO
                {
                    Id = int.Parse(reader["id_post"].ToString()),
                    IdPagina = int.Parse(reader["id_pagina"].ToString()),
                    IdCidade = int.Parse(reader["id_cidade"].ToString()),
                    id = reader["id_redesocial"].ToString(),
                    link = reader["link"].ToString(),
                    Data = DateTime.ParseExact(data, "dd/MM/yyyy HH:mm:ss", null),
                    message = reader["titulo"].ToString(),
                };
                list.Add(post);
            }
            Conexao.Connection.Close();
            return list;
        }

        public static async Task<List<PostFacebookDAO>> BuscarPostsPaginaAsync(long idPagina, int ultimosDias = 0)
        {
            var posts = default(List<PostFacebookDAO>);
            await Task.Run(() =>
            {
                posts = BuscarPostsPagina(idPagina, ultimosDias);
            });
            return posts;
        }
    }
}
