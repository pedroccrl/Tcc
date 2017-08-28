using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Shared.Model.Database.MySQL
{
    public class CidadeDAO : Cidade
    {
        public int Id { get; set; }

        public CidadeDAO(string nome, string uf) : base(nome, uf)
        {
        }

        
        
    }
}
