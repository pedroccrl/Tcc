using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Shared.Model.Facebook
{
    public class Post
    {
        public Comments comments { get; set; }
        public string id { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Comments
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

    
}
