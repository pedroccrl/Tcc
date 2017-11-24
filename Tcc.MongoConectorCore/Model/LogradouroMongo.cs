using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcc.MySQL.Model;

namespace Tcc.MongoConector.Model
{
    public class LogradouroMongo : LogradouroDAO
    {
        public List<ComentarioFacebookDAO> comentarios { get; set; }
    }
}
