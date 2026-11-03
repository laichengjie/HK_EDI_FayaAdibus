using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Models
{
    public class UnifyResultModel
    {
        public UnifyResultModel()
        {
            Status = new ResponseStatus();
        }
        public ResponseStatus Status { get; set; }

        public object Data { get; set; }

        public class ResponseStatus
        {
            public string code { get; set; }
            public string message { get; set; }
        }
    }
}