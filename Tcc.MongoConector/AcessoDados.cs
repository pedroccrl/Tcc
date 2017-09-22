using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
