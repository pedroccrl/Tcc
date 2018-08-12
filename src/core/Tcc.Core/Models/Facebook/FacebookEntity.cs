using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Core.Models.Facebook
{
    public class FacebookEntity : Entity
    {
        public string FacebookId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }
}
