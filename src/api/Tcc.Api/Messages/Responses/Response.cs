using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Messages.Responses
{
    public abstract class Response
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
