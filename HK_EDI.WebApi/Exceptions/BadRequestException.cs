using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Exceptions
{
    public class BadRequestException : ApiBaseException
    {
        public BadRequestException()
            : base("请求无效")
        {
            this.ResultCode = ApiErrorCode.BadRequest;
        }

        public BadRequestException(String message)
            : base(message)
        {
            ResultCode = ApiErrorCode.BadRequest;
        }

        public BadRequestException(String message, System.Exception innerException)
            : base(message, innerException)
        {
            ResultCode = ApiErrorCode.BadRequest;
        }
    }
}