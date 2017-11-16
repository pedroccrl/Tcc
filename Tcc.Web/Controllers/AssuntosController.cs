using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using Tcc.MySQL.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tcc.Web.Controllers
{
    [Route("api/[controller]")]
    public class AssuntosController : Controller
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

            var cid_col = database.GetCollection<CidadeAssunto>("cidades");
            var filtro = Builders<CidadeAssunto>.Filter.Where(c => c.id_cidade == cidadeDao.Id);
            var assuntos = cid_col.Find(filtro).ToList();
            var assunto = new
            {
                temas = new List<KeyValuePair<string,int>>(),
                qualidades = new List<KeyValuePair<string, int>>(),
            };
            foreach (var item in assuntos[0].temas)
            {
                if (item[0].AsString.Length < 3) continue;
                assunto.temas.Add(new KeyValuePair<string, int>(item[0].AsString, item[1].AsInt32));
            }
            foreach (var item in assuntos[0].qualidades)
            {
                if (item[0].AsString.Length < 3) continue;
                assunto.qualidades.Add(new KeyValuePair<string, int>(item[0].AsString, item[1].AsInt32));
            }
            return assunto;
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

        public class CidadeAssunto
        {
            public ObjectId Id { get; set; }
            public int id_cidade { get; set; }
            public List<BsonArray> temas { get; set; }
            public List<BsonArray> qualidades { get; set; }
        }
    }

}
