using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Models.Facebook
{
    public class Page : FacebookEntity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public ObjectId CidadeId { get; set; }

        public Page(string name, string link, ObjectId cidadeId)
        {
            Name = name;
            Link = link;
            CidadeId = cidadeId;
        }
    }
}
