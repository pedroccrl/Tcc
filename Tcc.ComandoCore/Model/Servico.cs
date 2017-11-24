using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.MySQL.Model;
using Tcc.Nucleo.Model.Facebook;
using static System.Console;
namespace Tcc.Comando.Model
{
    public class Servico : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _messageLoop;

        public string MessageLoop
        {
            get { return _messageLoop; }
            set
            {
                var msg = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - {value}\n";
                _messageLoop += msg;
                File.AppendAllText($"log_{DateTime.Now:yyyyMMdd}.txt", msg + "\n");
                Notify("Message");
                WriteLine(msg);
            }
        }

        public async Task InitAsync()
        {
            while (true)
            {
                int dias = 30;
                MessageLoop = "Buscando cidades no MySQL...";
                var cidades = await CidadeDAO.BuscarCidadesAsync();
                MessageLoop = $"{cidades.Count} cidades encontradas";
                foreach (var cidade in cidades)
                {
                    var paginas = await PaginaFacebookDAO.ObterPaginasCidadeAsync(cidade.Nome);
                    MessageLoop = $"{paginas.Count} páginas encontradas de {cidade.Nome}";
                    var dataRecente = $"{DateTime.Now.AddDays(-dias):dd/MM/yyyy}";
                    foreach (var pagina in paginas)
                    {
                        MessageLoop = "Procurando posts na API do Facebook";
                        var posts = await FacebookRestAPI.ObterPostsPaginaAsync(pagina.id, dataRecente);
                        MessageLoop = $"{posts.data.Count} encontrados nos ultimos {dias} dias";
                        int postsatt = 0;
                        foreach (var post in posts.data)
                        {
                            // Adiciona os novos posts ao mysql
                            var idPost = await PostFacebookDAO.AdicionarPostIdAsync(post, cidade.Id, pagina.Id);
                            if (idPost != -1) postsatt++;
                        }
                        MessageLoop = $"{postsatt} novos posts adicionados ao MySQL\nObtendo posts do MySQL";
                        // Busca todos os posts do mysql
                        var postsBd = PostFacebookDAO.BuscarPostsPagina(pagina.Id, dias);
                        MessageLoop = $"{postsBd.Count} posts encontrados\nAtualizando comentarios...\n";
                        int comentatt = 0;
                        foreach (var post in postsBd)
                        {
                            Progresso(postsBd.Count, postsBd.IndexOf(post) + 1, "Posts verificados.");
                            var idPost = post.Id;
                            var comentarios = await FacebookRestAPI.ObterComentariosPostAsync(post.id, dataRecente);
                            foreach (var comentario in comentarios)
                            {
                                var idComentario = await ComentarioFacebookDAO.AdicionarComentarioAsync(comentario, idPost, pagina.Id, cidade.Id);
                                if (idComentario != -1)
                                    comentatt++;
                                if (comentario.comments != null && comentario.comments.data.Count > 0)
                                {
                                    foreach (var comentRespondido in comentario.comments?.data)
                                    {
                                        idComentario = await ComentarioFacebookDAO.AdicionarComentarioAsync(comentRespondido, idPost, pagina.Id, cidade.Id, idComentario);
                                        if (idComentario != -1)
                                            comentatt++;
                                    }
                                }
                            }
                        }
                        MessageLoop = $"{comentatt} novos comentarios adicionados ao MySQL";
                        var comentariosCidade = ComentarioFacebookDAO.BuscarTodosComentariosDaCidade(cidade, dias);
                        int catt = 0, catual = 0;
                        foreach (var comentario in comentariosCidade)
                        {
                            catual++;
                            if (MongoConector.AcessoDados.SalvarUmComentarioSeNaoExistir(comentario, "comentarios"))
                            {
                                catt++;
                            }
                            Progresso(comentariosCidade.Count, catual, "Comentarios analisados no mongo");
                        }
                        MessageLoop = $"{catt} novos comentarios adicionados ao MongoDB";
                        ReadKey();
                    }

                }
                MessageLoop = "Dormindo por 1 hora...";
                await Task.Delay(1000 * 60 * 60);
            }
        }

        void Progresso(int total, int atual, string final = "")
        {
            SetCursorPosition(0, CursorTop - 1);
            var progress = $"{atual}/{total} - {((atual * 1.0) / total) * 100:0.0}% {final}";
            WriteLine(progress);
        }

        void Notify(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
