using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.MySQL.Model;
namespace Tcc.MongoConector
{
    public class AcessoDados
    {
        public static void SalvarComentarios(List<Tcc.MySQL.Model.ComentarioFacebookDAO> comentarios, string colecao)
        {
            try
            {
                var mongo = new MongoClient("mongodb://localhost:27017");
                var database = mongo.GetDatabase("dados_tcc");

                var comm_coll = database.GetCollection<MySQL.Model.ComentarioFacebookDAO>(colecao);
                
                comm_coll.InsertMany(comentarios);
            }
            catch (Exception e)
            {
                
            }
            
        }

        public static void SalvarComentariosSeNaoExistir(List<ComentarioFacebookDAO> comentarios, string colecao)
        {
            try
            {
                var mongo = new MongoClient("mongodb://localhost:27017");
                var database = mongo.GetDatabase("dados_tcc");

                var comm_coll = database.GetCollection<ComentarioFacebookDAO>(colecao);

                //comm_coll.InsertMany(comentarios);
                foreach (var comentario in comentarios)
                {
                    var filtro = Builders<ComentarioFacebookDAO>.Filter.Where(c => c.id != comentario.id);
                    var result = comm_coll.ReplaceOne(filtro, comentario, new UpdateOptions { IsUpsert = true });

                }

                
            }
            catch (Exception e)
            {

            }
        }

        public static bool SalvarUmComentarioSeNaoExistir(ComentarioFacebookDAO comentario, string colecao)
        {
            try
            {
                var mongo = new MongoClient("mongodb://localhost:27017");
                var database = mongo.GetDatabase("dados_tcc");

                var comm_coll = database.GetCollection<ComentarioFacebookDAO>(colecao);
                comm_coll.Indexes.CreateOne(Builders<ComentarioFacebookDAO>.IndexKeys.Text("_id"), new CreateIndexOptions { Unique = true });

                //comm_coll.InsertMany(comentarios);
                //var filtro = Builders<ComentarioFacebookDAO>.Filter.Where(c => c.id != comentario.id);
                //var result = comm_coll.ReplaceOne(filtro, comentario, new UpdateOptions { IsUpsert = true });

                comm_coll.InsertOne(comentario);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static void SalvarLogradourosSplit(List<Tcc.MySQL.Model.LogradouroDAO> logradouros)
        {
            var group = logradouros.GroupBy(l => l.Nome.Split(' ')[0]);
            try
            {
                var mongo = new MongoClient("mongodb://localhost:27017");
                var database = mongo.GetDatabase("dados_tcc");

                foreach (var tipo in group)
                {
                    var col = database.GetCollection<MySQL.Model.LogradouroDAO>(tipo.Key.ToLower());
                    foreach (var logradouro in tipo)
                    {
                        col.InsertOne(logradouro);
                    }
                }
                
            }
            catch (Exception e)
            {
                
            }
        }

        public static void SalverLogradourosDoc(List<MySQL.Model.LogradouroDoc> logradouros)
        {
            try
            {
                var mongo = new MongoClient("mongodb://localhost:27017");
                var database = mongo.GetDatabase("dados_tcc");
                var col = database.GetCollection<MySQL.Model.LogradouroDoc>("logradouros");
                col.InsertMany(logradouros);
            }
            catch (Exception e)
            {
                
            }
        }

        public static void SalvarBairros(List<MySQL.Model.BairroDAO> bairros)
        {
            var mongo = new MongoClient("mongodb://localhost:27017");
            var database = mongo.GetDatabase("dados_tcc");
            var col = database.GetCollection<MySQL.Model.BairroDAO>("bairro");
            col.InsertMany(bairros);
        }

        public static void SalvarPalavrasMongo()
        {
            var palavras = MySQL.Model.PalavraDAO.BuscarTodasPalavras();
            var mongo = new MongoClient("mongodb://localhost:27017");
            var database = mongo.GetDatabase("dados_tcc");
            var col = database.GetCollection<MySQL.Model.PalavraDAO>("palavras");
            col.InsertMany(palavras);
        }
    }
}
