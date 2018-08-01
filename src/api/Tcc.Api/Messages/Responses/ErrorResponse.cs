using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Messages.Responses
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(string error)
        {
            Success = false;
            Errors = new List<string>
            {
                error
            };
        }
    }
}
