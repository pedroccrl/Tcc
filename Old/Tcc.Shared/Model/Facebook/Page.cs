using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Shared.Model.Facebook
{
    public class Page
    {
        public Posts posts { get; set; }
        public string id { get; set; }
    }

    public class Datum
    {
        public string created_time { get; set; }
        public string message { get; set; }
        public string id { get; set; }
        public string story { get; set; }
        public From from { get; set; }
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
    }

    public class Posts
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }
    
}
