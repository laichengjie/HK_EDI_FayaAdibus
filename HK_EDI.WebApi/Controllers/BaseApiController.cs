using HK_EDI.WebApi.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HK_EDI.WebApi.Controllers
{
    [ApiAuthorizeAttribute]
    public class BaseApiController : ApiController
    {

    }
}
