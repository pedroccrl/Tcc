using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Facebook
{
    public class PostComments
    {
        public Comments comments { get; set; }
        public string id { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Comment
    {
        public string id { get; set; }
        public string created_time { get; set; }
        public From from { get; set; }
        public string message { get; set; }
        public int like_count { get; set; }
        public Comments comments { get; set; }
    }

    public class Comments
    {
        public List<Comment> data { get; set; }
        public Paging paging { get; set; }
    }

    
}
