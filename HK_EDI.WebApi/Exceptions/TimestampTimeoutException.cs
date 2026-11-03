using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Exceptions
{
    public class TimestampTimeoutException : ApiBaseException
    {
        public TimestampTimeoutException()
            : base("时间戳超时")
        {
            this.ResultCode = ApiErrorCode.Timestamp_Timeout;
        }

        public TimestampTimeoutException(String message)
            : base(message)
        {
            ResultCode = ApiErrorCode.Timestamp_Timeout;
        }

        public TimestampTimeoutException(String message, System.Exception innerException)
            : base(message, innerException)
        {
            ResultCode = ApiErrorCode.Timestamp_Timeout;
        }
    }
}