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
    public class CidadeController : ControllerBase
    {
        private readonly IMongoCollection<Cidade> CidadeCollection;

        public CidadeController()
        {
            CidadeCollection = Connection.Database.GetCollection<Cidade>("cidades");
        }

        [HttpGet]
        public object GetCidades()
        {
            var cidades = CidadeCollection.Find(_ => true).ToList();

            return cidades;
        }

        [HttpGet("{id}")]
        public object GetCidade(int id)
        {
            var cidades = CidadeCollection.Find(c => c.IdCidade == id).FirstOrDefault();

            return cidades;
        }
    }
}