using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Exceptions
{
    public class SystemErrorException : ApiBaseException
    {
        public SystemErrorException()
            : base("系统错误")
        {
            this.ResultCode = ApiErrorCode.SystemError;
        }

        public SystemErrorException(String message)
            : base(message)
        {
            ResultCode = ApiErrorCode.SystemError;
        }

        public SystemErrorException(String message, System.Exception innerException)
            : base(message, innerException)
        {
            ResultCode = ApiErrorCode.SystemError;
        }
    }
}