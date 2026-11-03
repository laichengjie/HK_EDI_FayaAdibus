using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Exceptions
{
    public class NotAuthorizeException : ApiBaseException
    {
        public NotAuthorizeException()
            : base("无权访问")
        {
            this.ResultCode = ApiErrorCode.NotAuthorize;
        }

        public NotAuthorizeException(String message)
            : base(message)
        {
            ResultCode = ApiErrorCode.NotAuthorize;
        }

        public NotAuthorizeException(String message, System.Exception innerException)
            : base(message, innerException)
        {
            ResultCode = ApiErrorCode.NotAuthorize;
        }
    }
}