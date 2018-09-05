using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Tcc.Api.Models;

namespace Tcc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IMongoCollection<Config> ConfigCollection;

        public ConfigController()
        {
            ConfigCollection = Connection.Database.GetCollection<Config>("config");
        }

        [HttpGet]
        public object GetConfig()
        {
            var config = ConfigCollection.Find(_ => true).FirstOrDefault();

            return config;
        }
    }
}