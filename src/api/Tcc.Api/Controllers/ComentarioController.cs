using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tcc.Api.Models;

namespace Tcc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IMongoCollection<Comentario> ComentarioCollection;

        public ComentarioController()
        {
            ComentarioCollection = Connection.Database.GetCollection<Comentario>("comentarios_original");
        }

        [HttpGet]
        public object GetComentarios()
        {
            var comentarios = ComentarioCollection.AsQueryable()
                .Take(100)
                .ToList();

            return comentarios;
        }

        [HttpGet("cidade/{id}")]
        public object GetComentariosCidade(int id)
        {
            var comentarios = from coll in ComentarioCollection.AsQueryable()
                              where coll.TemLogradouro == true && coll.Logradouros.Count() > 0 && coll.IdCidade == id
                              select coll;

            return comentarios.ToList();
        }
    }
}