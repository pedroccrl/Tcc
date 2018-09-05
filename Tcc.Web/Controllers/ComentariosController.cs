using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tcc.MySQL.Model;

namespace Tcc.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Comentarios")]
    public class ComentariosController : Controller
    {
        [HttpGet("{cidade}")]
        public object Get(string cidade)
        {
            int page = int.Parse(Request.Query["page"]);
            var mongo = new MongoClient("mongodb://localhost:27017");
            var database = mongo.GetDatabase("dados_tcc");

            var cidadeDao = CidadeDAO.BuscarCidade(cidade);

            var def = new
            {
                _id = "",
                created_time = "",
                from = new
                {
                    name = ""
                },
                message = "",
                like_count = 0,
                corrigido = "",

            };
            var com_col = database.GetCollection<ComentarioOriginal>("comentarios_original");
            var filtro = Builders<ComentarioOriginal>.Filter.Where(c => c.IdCidade == cidadeDao.Id && c.TemLogradouro==true);
            var comentarios = com_col.Find(filtro).ToList();
            var qt = 50;
            int totalPage = comentarios.Count / qt;
            var pular = (page - 1) * qt;
            comentarios = comentarios.Skip(pular).Take(qt).ToList();
            var result = new
            {
                comentarios = comentarios,
                pagina = page,
                total = totalPage
            };
            return comentarios;
        }
    }


    public class ComentarioOriginal
    {
        public string _id { get; set; }
        public string created_time { get; set; }
        public From from { get; set; }
        public string message { get; set; }
        public int like_count { get; set; }
        public object comments { get; set; }
        public int IdComentario { get; set; }
        public object IdRespondido { get; set; }
        public int IdCidade { get; set; }
        public int IdPagina { get; set; }
        public int IdPost { get; set; }
        public string[] palavras_unk { get; set; }
        public string corrigido { get; set; }
        public string[] palavras_unk_c { get; set; }
        public Tema tema { get; set; }
        public bool TemLogradouro { get; set; }
        public Logradouro[] Logradouros { get; set; }
    }

    public class From
    {
        public string _id { get; set; }
        public string name { get; set; }
    }

    public class Tema
    {
        public string[] qualidades { get; set; }
        public string[] nomes { get; set; }
        public string[][][] pos_tags { get; set; }
    }

    public class Logradouro
    {
        public int IdBairro { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public double? Longitude { get; set; }
        public int _id { get; set; }
        public double? Latitude { get; set; }
        public string Tipo { get; set; }
        public Bairro Bairro { get; set; }
        public int IdCidade { get; set; }
    }

    public class Bairro
    {
        public string NomeAlternativo { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
    }

}