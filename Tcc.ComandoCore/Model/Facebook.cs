using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.MySQL.Model;
using Tcc.Nucleo.Model.Facebook;
using static System.Console;
namespace Tcc.Comando.Model
{
    public static class Facebook
    {
        public static async Task AdicionarPosts()
        {
            var paginas = PaginaFacebookDAO.ObterTodasPaginas();
            WriteLine("Para buscar posts aperte TAB e o id da pagina.\n");
            WriteLine("ID\tId Cidade\tNome");
            foreach (var page in paginas)
            {
                WriteLine($"{page.Id}\t{page.IdCidade}\t{page.name}");
            }
            if (ReadKey().Key == ConsoleKey.Tab)
            {
                var id = ReadLine();
                var page = paginas.Find(p => p.Id == int.Parse(id));

                WriteLine("Buscar desde que data no formato DD/MM/YYYY (deixe em branco para qualquer): ");
                var data = ReadLine();

                var posts = new Posts();
                var postlist = new List<Post>();
                WriteLine($"Procurando posts em {page.name} desde {data}...");
                WriteLine("Aguarde...");
                posts = await FacebookRestAPI.ObterPostsPaginaAsync(page.id, data);
                postlist.AddRange(posts.data);
                SetCursorPosition(0, CursorTop - 1);
                WriteLine($"{postlist.Count} posts encontrados. Aguarde...");
                while (!string.IsNullOrWhiteSpace(posts.paging.next))
                {
                    posts = await FacebookRestAPI.GetObjectAsync<Posts>(posts.paging.next);
                    postlist.AddRange(posts.data);
                    SetCursorPosition(0, CursorTop - 1);
                    WriteLine($"{postlist.Count} posts encontrados. Aguarde...");
                }
                SetCursorPosition(0, CursorTop - 1);
                WriteLine($"Fim da busca, {postlist.Count} posts encontrados.");
                WriteLine("F1 - Adicionar posts ao Banco");
                if (ReadKey().Key == ConsoleKey.F1)
                {
                    int i = 0;
                    int t = 0;
                    double total = postlist.Count;
                    foreach (var post in postlist)
                    {
                        var ok = PostFacebookDAO.AdicionarPost(post, page.IdCidade, page.Id);
                        if (ok) i++;
                        t++;
                        SetCursorPosition(0, CursorTop - 1);
                        var porcento = (t / total) * 100;
                        WriteLine($"{i} adicionados. {porcento:0.0}% concluido");
                    }
                }
                WriteLine("Pressione qualquer tecla para sair");
                Read();
            }
        }

        public static async Task AdicionarPagina()
        {
            WriteLine("Cidade referente a pagina: ");
            var cidade = ReadLine();
            WriteLine("ID da pagina: ");
            var id = ReadLine();
            WriteLine("Buscando pagina...");
            var page = await FacebookRestAPI.ObterPaginaAsync(id);
            if (page != null)
            {
                WriteLine($"Pagina {page.name} encontrada");
                WriteLine("F1 - Adicionar pagina ao Banco");
                if (ReadKey().Key == ConsoleKey.F1)
                {
                    var res = PaginaFacebookDAO.AdicionarPagina(page, cidade);
                    if (!res)
                    {
                        WriteLine("Houve um erro. F1 para tentar novamente");
                        if (ReadKey().Key == ConsoleKey.F1)
                        {
                            AdicionarPagina().Wait();
                        }
                    }
                }
            }
            else
            {
                WriteLine("Houve um erro. F1 para tentar novamente");
                if (ReadKey().Key == ConsoleKey.F1)
                {
                    AdicionarPagina().Wait();
                }
            }
        }

        public static async Task AdicionarComentarios()
        {
            WriteLine("Cidade referente as paginas: ");
            var cidade = ReadLine();
            var paginas = PaginaFacebookDAO.ObterPaginasCidade(cidade);
            WriteLine($"{paginas.Count} paginas encontradas. \nObtendo posts...");
            var posts = new List<PostFacebookDAO>();
            foreach (var p in paginas)
            {
                posts.AddRange(PostFacebookDAO.BuscarPostsPagina(p.Id));
                SetCursorPosition(0, CursorTop - 1);
                WriteLine($"{posts.Count} encontrados. Aguarde...");
            }
            SetCursorPosition(0, CursorTop - 1);
            WriteLine($"Fim da busca. {posts.Count} encontrados.");

            WriteLine($"Obtendo comentarios do facebook. Aguarde...");
            double total = 0;
            int comentariosCount = 0;
            
            SetCursorPosition(0, CursorTop - 1);
            WriteLine("0 Comentários encontrados. Aguarde...");
            foreach (var post in posts)
            {
                double totalPorc = (total / posts.Count) * 100;

                var comentarios = await FacebookRestAPI.ObterComentariosPostAsync(post.id);
                comentariosCount += comentarios.Count;
                
                SetCursorPosition(0, CursorTop - 1);
                WriteLine($"{comentarios.Count} Comentários encontrados. {totalPorc:0.0}% concluido");
                
                double parcial = 0;
                int add = 0;
                int addR = 0;
                foreach (var comment in comentarios)
                {
                    long ok = ComentarioFacebookDAO.AdicionarComentario(comment, post.Id, post.IdPagina, post.IdCidade);
                    try
                    {
                        if (ok != -1) add++;
                        if (comment.comments != null)
                        {
                            foreach (var com_r in comment?.comments?.data)
                            {
                                ok = ComentarioFacebookDAO.AdicionarComentario(com_r, post.Id, post.IdPagina, post.IdCidade, ok);
                                if (ok != -1) addR++;
                            }
                        }
                        parcial++;
                        double parcialPorc = (parcial / comentarios.Count) * 100;
                        //SetCursorPosition(0, CursorTop  -1);
                        //WriteLine($" | {add} comentarios e {addR} respostas foram adicionadas ao bd. {parcialPorc:0.0}% concluido");
                    }
                    catch (Exception e)
                    {
                        
                    }
                    
                }
                total++;
                
            }
            SetCursorPosition(0, CursorTop - 1);
            WriteLine($"Fim da busca. {comentariosCount} Comentários encontrados.");

            Read();
        }
    }
}
