using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tcc.Api.Messages.Cidades;
using Tcc.Api.Messages.Responses;
using Tcc.Core.Models;

namespace Tcc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        IMongoCollection<Cidade> cidadeCollection;

        public CidadeController()
        {
            cidadeCollection = Entity.GetCollection<Cidade>();
        }

        [HttpPost]
        public IActionResult New([FromBody] CidadePostRequest cidadePostRequest)
        {
            var cidade = Mapper.Map<Cidade>(cidadePostRequest);

            cidadeCollection.InsertOne(cidade);

            return Ok(new DataResponse("Ok"));
        }
    }
}
