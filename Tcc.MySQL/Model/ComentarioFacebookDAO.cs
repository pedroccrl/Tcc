using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.Nucleo.Model.Facebook;

namespace Tcc.MySQL.Model
{
    public class ComentarioFacebookDAO : Comment
    {
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

        public static List<Comment> BuscarTodosComentarios()
        {
            var query = "SELECT * FROM comentario";
            var cmd = new MySqlCommand(query, Conexao.Connection);
            var reader = cmd.ExecuteReader();
            var comentarios = new List<Comment>();
            while (reader.Read())
            {
                var c = new Comment
                {
                    message = reader["mensagem"].ToString(),
                };
                comentarios.Add(c);
            }
            Conexao.Connection.Close();
            return comentarios;
        }
    }
}
