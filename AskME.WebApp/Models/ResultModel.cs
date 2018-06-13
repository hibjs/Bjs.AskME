using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AskME.WebApp.Models
{
    public class ResultModel
    {
        public HttpStatusCode Status { get; set; }

        [DefaultValue("")]
        public Object Data { get; set; }

        public string Msg { get; set; }
    }
}
