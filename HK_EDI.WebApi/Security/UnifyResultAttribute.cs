using HK_EDI.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using HK_EDI.WebApi.Exceptions;

namespace HK_EDI.WebApi.Security
{
    public class UnifyResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
                return;
            base.OnActionExecuted(actionExecutedContext);
            UnifyResultModel result = new UnifyResultModel();
            result.Status.code = "0";
            if (actionExecutedContext.ActionContext.Response.Content != null)
            {
                result.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
            }
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                actionExecutedContext.ActionContext.Response.StatusCode, result);
        }
    }
}