using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Core.Models;
using System.Linq;

namespace Tcc.Service.Tasks
{
    public static class PlaceFinderTask
    {
        public static async Task BuscaCidadesCorreios()
        {
            var cidadeColl = Entity.GetCollection<Cidade>();
            var bairroColl = Entity.GetCollection<Bairro>();
            var logradouroColl = Entity.GetCollection<Logradouro>();

            var cidadesQuery = from c in cidadeColl.AsQueryable()
                               join b in bairroColl.AsQueryable() on c.Id equals b.CidadeId into cidadeBairros
                               select new
                               {
                                   Cidade = c,
                                   Bairros = cidadeBairros
                               };

            var cidades = cidadesQuery.ToList().Select(c => new Cidade
            {
                Nome = c.Cidade.Nome,
                UF = c.Cidade.UF,
                Id = c.Cidade.Id,
                Localization = c.Cidade.Localization,
                Bairros = c.Bairros.Where(b => !b.Completo).ToList()
            }).ToList();

            foreach (var cidade in cidades)
            {
                foreach (var bairro in cidade.Bairros)
                {
                    var b = await Core.Services.CorreiosService.GetLogradouroPorBairroAsync(cidade, bairro.Nome);
                    var logradouros = b.Logradouros.Select(br => new Logradouro
                    {
                        Nome = br.Nome,
                        Cep = br.Cep,
                        BairroId = bairro.Id,
                        CidadeId = cidade.Id
                    }).ToList();

                    try
                    {
                        foreach (var logradouro in logradouros)
                        {
                            await logradouroColl.InsertOneAsync(logradouro);
                        }
                    }
                    catch (MongoWriteException e)
                    {
                        if (e.WriteError.Category == ServerErrorCategory.DuplicateKey)
                            Console.WriteLine("Duplicado.");
                    }

                    Console.WriteLine($"{b.Logradouros.Count} Logradouros inseridos do Bairro {bairro.Nome} da Cidade {cidade.Nome}.");

                    bairro.Completo = true;

                    await bairroColl.ReplaceOneAsync(ba => ba.Id == bairro.Id, bairro);
                }
            }
        }
    }
}
