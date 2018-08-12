using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Models.Facebook
{
    public class Post : FacebookEntity
    {
        public string Message { get; set; }
        public string Link { get; set; }
        public ObjectId CidadeId { get; set; }
        public ObjectId PageId { get; set; }
    }
}
