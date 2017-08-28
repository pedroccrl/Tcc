using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Facebook
{
    public class FacebookRestAPI
    {
        static string GraphUrl = "https://graph.facebook.com/v2.10/";

        public static string AccessToken = "1297744340351161|waLjupMYyRknsbTncMd3ysA53uw";

        public static async Task<Page> ObterPaginaAsync(string id)
        {
            var page = await GetObjectAsync<Page>($"{GraphUrl}300857150016905?fields=name%2Cid%2Clink&access_token={AccessToken}");

            return page;
        }

        public static async Task<Posts> ObterPostsPagina(string idPagina, string since = "0", int limit = 100)
        {
            var url = $"{GraphUrl}{idPagina}/posts?fields=message,id,created_time,link&limit={limit}&access_token={AccessToken}";
            if (since != "0" || !string.IsNullOrWhiteSpace(since))
            {
                var data = (DateTime.ParseExact(since, "dd/MM/yyyy", null) - new DateTime(1970, 01, 01));
                var timespan = new TimeSpan(data.Ticks);
                url += $"&since={timespan.TotalSeconds}";
            }
            else since = "0";
            var posts = await GetObjectAsync<Posts>(url);
            posts.Since = since;
            return posts;
        }

        public static async Task<List<Comment>> ObterComentariosPost (string idPost, int limit = 100)
        {
            var url = $"{GraphUrl}{idPost}?fields=comments.limit({limit}){"{id,created_time,from,message,like_count,comments.limit(100){id,created_time,from,message,like_count}}"}&access_token={AccessToken}";
            var comments = await GetObjectAsync<PostComments>(url);
            var next = comments?.comments?.paging?.next;
            var comentarios = new List<Comment>();
            try
            {
                comentarios.AddRange(comments?.comments?.data);

                while (!string.IsNullOrWhiteSpace(next))
                {
                    url = next;
                    var cmm = await GetObjectAsync<Comments>(url);
                    next = cmm?.paging?.next;
                    comentarios.AddRange(cmm.data);
                }
            }
            catch (Exception e)
            {
                
            }
            

            return comentarios;
        }

        public static async Task<T> GetObjectAsync<T>(string url)
        {
            var obj = default(T);
            string resposta = "";
            try
            {
                await Task.Run(async () =>
                {
                    var http = new HttpClient();
                    resposta = await http.GetStringAsync(url);

                    obj = JsonConvert.DeserializeObject<T>(resposta);
                });
            }
            catch (Exception e)
            {
                
            }

            return obj;
        }
    }
}
