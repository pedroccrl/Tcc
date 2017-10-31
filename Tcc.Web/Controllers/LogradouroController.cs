using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tcc.MongoConector.Model;
using MongoDB.Driver;

namespace Tcc.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Logradouro")]
    public class LogradouroController : Controller
    {
        // GET: api/Logradouro
        [HttpGet]
        public IEnumerable<LogradouroMongo> Get()
        {
            var mongo = new MongoClient("mongodb://localhost:27017");
            var database = mongo.GetDatabase("dados_tcc");

            var builder = Builders<LogradouroMongo>.Filter;
            var filt = builder.Where(a => a.Latitude!=null && a.comentarios.Count > 0);

            var col = database.GetCollection<LogradouroMongo>("rua");

            var list = col.Find<LogradouroMongo>(filt).ToList();
            return list;
        }

        // GET: api/Logradouro/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Logradouro
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Logradouro/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
