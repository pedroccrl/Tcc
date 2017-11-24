using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Nucleo.Model.Facebook
{
    public class Page
    {
        public Posts posts { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Post
    {
        public string created_time { get; set; }
        public string message { get; set; }
        public string id { get; set; }
        public string link { get; set; }
        public DateTime Data { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
    }

    public class Posts
    {
        public List<Post> data { get; set; }
        public Paging paging { get; set; }
        private string _since;

        public string Since
        {
            get { return _since; }
            set
            {
                _since = value;
                if (paging != null)
                {
                    if (!string.IsNullOrWhiteSpace(paging.next)) paging.next += "&since=" + _since;
                }
            }
        }

    }
    
}
