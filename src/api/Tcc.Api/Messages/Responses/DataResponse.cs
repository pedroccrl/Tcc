using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Messages.Responses
{
    public class DataResponse : Response
    {
        public DataResponse(object data)
        {
            Data = data;
            Success = true;
            Errors = new List<string>();
        }

        public object Data { get; set; }
    }
}
