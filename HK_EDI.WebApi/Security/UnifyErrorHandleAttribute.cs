using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using HK_EDI.WebApi.Models;
using HK_EDI.WebApi.Exceptions;
using HK_EDI.Core.Infrastructure;
using Nop.Services.Logging;
using HK_EDI.Services.Logging;

namespace HK_EDI.WebApi.Security
{
    public class UnifyErrorHandleAttribute : ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            var errorMessage = actionExecutedContext.Exception.Message;
            var result = new UnifyResultModel();
            result.Status.message = errorMessage;
            if (actionExecutedContext.Exception is ApiBaseException)
            {
                result.Status.code = (actionExecutedContext.Exception as ApiBaseException).ResultCode;
            }
            else
            {
                var _logger = EngineContext.Current.Resolve<ILogger>();
                _logger.Error(errorMessage, actionExecutedContext.Exception);
                result.Status.code = ApiErrorCode.SystemError;
            }
            System.Net.HttpStatusCode code;
            if (actionExecutedContext.ActionContext.Response == null)
                code = System.Net.HttpStatusCode.Accepted;
            else
                code = actionExecutedContext.ActionContext.Response.StatusCode;
            actionExecutedContext.Response = actionExecutedContext.Request
                .CreateResponse(code, result);
        }
    }
}