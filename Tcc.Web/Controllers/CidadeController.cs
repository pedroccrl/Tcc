using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tcc.MySQL.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tcc.Web.Controllers
{
    [Route("api/[controller]")]
    public class CidadeController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{nome}")]
        public object Get(string nome)
        {
            var cidade = CidadeDAO.BuscarCidade(nome);
            var pags = PaginaFacebookDAO.ObterPaginasCidade(cidade.Nome);
            var comentarios = ComentarioFacebookDAO.BuscarTodosComentariosDaCidade(cidade);
            var posts = 0;
            var logradouros = LogradouroDAO.BuscarTodosLogradouros(nome);
            foreach (var item in pags)
            {
                posts += PostFacebookDAO.BuscarPostsPagina(item.Id).Count;
            }
            var c = new
            {
                cidade = cidade.Nome.ToUpper(),
                bairros = cidade.BairrosDAO.Count,
                paginas = pags.Count,
                comentarios = comentarios.Count,
                posts = posts,
                logradouros = logradouros.Count
            };
            return c;
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
    }
}
