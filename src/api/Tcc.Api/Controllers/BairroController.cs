using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tcc.Api.Messages.Bairros;
using Tcc.Api.Messages.Responses;
using Tcc.Core.Models;

namespace Tcc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {
        IMongoCollection<Bairro> bairroCollection;
        public BairroController()
        {
            bairroCollection = Entity.GetCollection<Bairro>();
        }

        [HttpPost]
        public IActionResult New([FromBody] BairrosPostRequest bairroPostRequest)
        {
            foreach (var nome in bairroPostRequest.Bairros)
            {
                var bairro = new Bairro
                {
                    CidadeId = new MongoDB.Bson.ObjectId(bairroPostRequest.CidadeId),
                    Nome = nome
                };

                bairroCollection.InsertOne(bairro);
            }

            return Ok(new DataResponse($"{bairroPostRequest.Bairros.Count()} adicionado."));
        }
    }
}