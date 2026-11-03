using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Exceptions
{
    public class ApiBaseException : System.Exception
    {
        public string ResultCode { get; set; }

        public ApiBaseException() 
            : base() {
                ResultCode = ApiErrorCode.SystemError;
        }
        
        public ApiBaseException(String message) 
            : base(message) {
                ResultCode = ApiErrorCode.SystemError;
        }

        public ApiBaseException(String message, System.Exception innerException) 
            : base(message, innerException) {
                ResultCode = ApiErrorCode.SystemError;
        }
    }
}