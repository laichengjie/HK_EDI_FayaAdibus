using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Exceptions
{
    public class ErrorArgumentException : ApiBaseException
    {
        public ErrorArgumentException()
            : base("参数错误")
        {
            this.ResultCode = ApiErrorCode.ErrorArgument;
        }

        public ErrorArgumentException(String message)
            : base(message)
        {
            ResultCode = ApiErrorCode.ErrorArgument;
        }

        public ErrorArgumentException(String message, System.Exception innerException)
            : base(message, innerException)
        {
            ResultCode = ApiErrorCode.ErrorArgument;
        }
    }
}