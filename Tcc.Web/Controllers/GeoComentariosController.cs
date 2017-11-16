using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tcc.MySQL.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tcc.Web.Controllers
{
    [Route("api/[controller]")]
    public class GeoComentariosController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{cidade}")]
        public object Get(string cidade)
        {
            var mongo = new MongoClient("mongodb://pedro-pc:27017");
            var database = mongo.GetDatabase("dados_tcc");
            var cidadeDao = CidadeDAO.BuscarCidade(cidade);

            var filter = Builders<GeoComentario>.Filter.Where(c => c.IdCidade == cidadeDao.Id && c.Logradouros.Count() >= 1);
            var coments = database.GetCollection<GeoComentario>("comentarios_original").Find(filter).ToList();

            var geocomentarios = new List<object>();
            foreach (var c in coments)
            {
                var geoco = new
                {
                    comentario = c.message,
                    id_comentario = c.IdComentario,
                    logradouro = c.Logradouros
                };
                geocomentarios.Add(geoco);
            }

            return geocomentarios;
        }

        [HttpGet("{cidade}/{bairro}")]
        public object Get(string cidade, string bairro)
        {
            bairro = bairro.ToUpper();
            var mongo = new MongoClient("mongodb://pedro-pc:27017");
            var database = mongo.GetDatabase("dados_tcc");
            var cidadeDao = CidadeDAO.BuscarCidade(cidade);
            var bairroDao = BairroDAO.BuscarTodosBairros(cidadeDao).Find(b => b.Nome == bairro || b.NomeAlternativo == bairro);

            var filter = Builders<GeoComentario>.Filter.Where(c => c.IdCidade == cidadeDao.Id && c.Logradouros.Count() >= 1);
            var coments = database.GetCollection<GeoComentario>("comentarios_original").Find(filter).ToList();

            var geocomentarios = new List<object>();
            foreach (var c in coments)
            {
                if (c.Logradouros.ToList().Find(l => l.IdBairro == bairroDao.Id) == null) continue;
                var geoco = new
                {
                    comentario = c.message,
                    id_comentario = c.IdComentario,
                    logradouro = c.Logradouros
                };
                geocomentarios.Add(geoco);
            }

            return geocomentarios;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        public class GeoComentario
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
            public string[][][] pos_tags { get; set; }
            public string[] nomes { get; set; }
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
}
