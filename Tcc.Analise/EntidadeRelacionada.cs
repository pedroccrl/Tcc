using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.MySQL.Model;
using Tcc.Nucleo.Model;
using Tcc.Nucleo.Model.Analise;

namespace Tcc.Analise
{
    public class EntidadeRelacionada : Entidade
    {
        public Relacao Relacao { get; set; }

        static List<BairroDAO> Bairros;
        static List<LogradouroDAO> Logradouros;
        static List<PaginaFacebookDAO> PaginasFb;
        static List<PostFacebookDAO> PostsFb;
        static List<ComentarioFacebookDAO> ComentariosFb;

        /// <summary>
        /// Busca os comentarios, posts, logradouros e bairros no banco de dados e relaciona-os
        /// </summary>
        /// <param name="cidade">Cidade a ser procurada</param>
        /// <returns>Lista de entidades encontradados no comentario ou post</returns>
        public static List<EntidadeRelacionada> ReconhecerEntididadesRelacionadas(string cidade)
        {
            Bairros = BairroDAO.BuscarTodosBairros(cidade);
            Logradouros = LogradouroDAO.BuscarTodosLogradouros(cidade);
            PaginasFb = PaginaFacebookDAO.ObterPaginasCidade(cidade);
            PostsFb = new List<PostFacebookDAO>();
            foreach (var pagina in PaginasFb)
                PostsFb.AddRange(PostFacebookDAO.BuscarPostsPagina(pagina.Id));
            ComentariosFb = ComentarioFacebookDAO.BuscarTodosComentarios();

            var entidades_rel = new List<EntidadeRelacionada>();

            var sw = new Stopwatch();
            sw.Start();
            foreach (var comentario in ComentariosFb)
            {
                var comm_f = Formatacao.FormatarTexto(comentario.message);
                var tokens = Token.Tokenize(comentario.message);
                var entidades = ReconhecerEntidades(comm_f.Formatado);
                foreach (var ent in entidades)
                {
                    var ent_rel = ReconhecerEntidadeRelacionada(ent, comm_f.Original);
                    if (ent_rel != null) entidades_rel.Add(ent_rel);
                }
            }

            foreach (var post in PostsFb)
            {
                var tokens = Token.Tokenize(post.message);
                var comm_f = Formatacao.FormatarTexto(post.message);
                var entidades = ReconhecerEntidades(comm_f.Formatado);
            }


            sw.Stop();
            

            return null;
        }

        static EntidadeRelacionada ReconhecerEntidadeRelacionada(Entidade entidade, string texto)
        {
            texto = StringAcento.RemoverAcento(texto.ToLower());
            var original = texto;
            if (entidade.Tipo== TipoEntidade.Local)
            {
                var sub = texto.Substring(entidade.Fim);
                if (entidade.Nome == "bairro" || entidade.Nome == "ruas" || entidade.Nome == "orla" || entidade.Nome == "praia")
                {
                    foreach (var bairro in Bairros)
                    {
                        var bairro_nome = StringAcento.RemoverAcento(bairro.Nome.ToLower());
                        int index = sub.IndexOf(bairro_nome);
                        if (index != -1)
                        {

                        }
                    }
                }
                else if (entidade.Nome == "rua")
                {

                }
            }

            return null;
        }
    }
}
