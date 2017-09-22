using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model.Facebook;
using Newtonsoft.Json;
using MongoDB.Driver;
namespace Tcc.MySQL.Model
{
    public class ComentarioFacebookDAO : Comment
    {
        [JsonProperty("id_comentario")]
        public long IdComentario { get; set; }
        [JsonProperty("id_comentario_respondido")]
        public long? IdRespondido { get; set; }
        [JsonProperty("id_cidade")]
        public long IdCidade { get; set; }
        [JsonProperty("id_pagina")]
        public long IdPagina { get; set; }
        [JsonProperty("id_post")]
        public long IdPost { get; set; }

        public static long AdicionarComentario(Comment comentario, long idPost, long idPagina, long idCidade, long idComentarioRespondido = -1)
        {
            string query = "";
            try
            {
                Conexao.Connection.Close();
                query = "INSERT INTO comentario (id_post, id_pagina, id_cidade, id_autor, nome_autor, mensagem, data, id_redesocial, like_count";
                if (idComentarioRespondido != -1) query += ", id_comentario_respondido)";
                else query += ")";
                query += $" VALUES ({idPost}, {idPagina}, {idCidade}, {comentario.from.id}, '{comentario.from.name}', '{comentario.message}', @Data, '{comentario.id}', {comentario.like_count}";
                if (idComentarioRespondido != -1) query += $", {idComentarioRespondido})";
                else query += ")";
                var cmd = new MySqlCommand(query, Conexao.Connection);
                var date = DateTime.ParseExact(comentario.created_time, "yyyy-MM-ddTHH:mm:ss+0000", null);
                var datetimeMysql = $"{date:yyyy-MM-dd HH:mm:ss}";
                cmd.Parameters.AddWithValue("@Data", datetimeMysql);

                var r = cmd.ExecuteNonQuery();
                var id = cmd.LastInsertedId;
                Conexao.Connection.Close();
                return id;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static List<ComentarioFacebookDAO> BuscarTodosComentarios()
        {
            var query = "SELECT * FROM comentario";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            var reader = cmd.ExecuteReader();
            var comentarios = new List<ComentarioFacebookDAO>();
            while (reader.Read())
            {
                var c = new ComentarioFacebookDAO
                {
                    message = reader["mensagem"].ToString(),
                    IdCidade = long.Parse(reader["id_cidade"].ToString()),
                    IdPagina = long.Parse(reader["id_pagina"].ToString()),
                    IdPost = long.Parse(reader["id_post"].ToString()),
                    //IdRespondido = long.Parse(reader["id_comentario_respondido"].ToString()),
                    IdComentario = long.Parse(reader["id_comentario"].ToString()),
                    id = reader["id_redesocial"].ToString(),
                    created_time = reader["data"].ToString(),
                    like_count = int.Parse(reader["like_count"].ToString()),
                    from = new From
                    {
                        id = reader["id_autor"].ToString(),
                        name = reader["nome_autor"].ToString()
                    }
                };
                comentarios.Add(c);
            }
            Conexao.Connection.Close();
            return comentarios;
        }
    }
}
