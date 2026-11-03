using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HK_EDI.WebApi.Controllers
{
    public class DefaultController : BaseApiController
    {
        // GET: api/Default
        [HttpGet, HttpPost]
        public string CheckToken()
        {
            return "";
        }

    }
}
