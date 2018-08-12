using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tcc.Api.Messages.Facebook;
using Tcc.Core.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Tcc.Api.Messages.Responses;
using Tcc.Core.Services;
using Tcc.Core.Models.Facebook;

namespace Tcc.Api.Controllers.ApiFacebook
{
    [Route("api/facebook/[controller]")]
    [ApiController]
    public class PaginaController : ControllerBase
    {
        public IMongoCollection<Cidade> CidadeCollection { get; set; }
        public IMongoCollection<Page> PageCollection { get; set; }

        public PaginaController()
        {
            CidadeCollection = Entity.GetCollection<Cidade>();
            PageCollection = Entity.GetCollection<Page>();
        }

        [HttpPost]
        public async Task<IActionResult> PostPage([FromBody] PagePostRequest pageRequest)
        {
            var cidadeObjId = new ObjectId(pageRequest.CidadeId);

            var cidade = CidadeCollection.AsQueryable().Where(c => c.Id == cidadeObjId).FirstOrDefault();
            if (cidade == null)
                return BadRequest(new ErrorResponse("Cidade não existe."));

            var pagef = await FacebookService.ObterPaginaAsync(pageRequest.PageId);
            if (pagef == null)
                return BadRequest(new ErrorResponse("Página não encontrada."));

            var page = new Page(pagef.name, pagef.link, cidade.Id);
            await PageCollection.InsertOneAsync(page);

            return Ok(new DataResponse(page));
        }
    }
}
