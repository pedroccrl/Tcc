using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tcc.Core.Mongo;
using AutoMapper;
using Tcc.Api.Messages.Cidades;
using Tcc.Core.Models;

namespace Tcc.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MongoSettings>(o =>
            {
                o.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                o.Database = Configuration.GetSection("MongoConnection:Database").Value;


            });

            services.AddAutoMapper(typeof(Startup));
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<CidadePostRequest, Cidade>().ReverseMap();
            });
            var mongoSettings = new MongoSettings
            {
                ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value,
                Database = Configuration.GetSection("MongoConnection:Database").Value
            };
            MongoConnection.Init(mongoSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
