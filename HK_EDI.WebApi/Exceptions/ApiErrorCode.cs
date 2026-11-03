using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.WebApi.Exceptions
{
    internal static  class ApiErrorCode
    {
        /// <summary>
        /// 系统繁忙
        /// </summary>
        internal const string SystemBusy = "1";
        /// <summary>
        /// 无权访问
        /// </summary>
        internal const string NotAuthorize = "1";
        /// <summary>
        /// 接口已关闭
        /// </summary>
        internal const string Interface_Is_Close = "1";
        /// <summary>
        /// 系统错误
        /// </summary>
        internal const string SystemError = "1";
        /// <summary>
        /// 参数错误
        /// </summary>
        internal const string ErrorArgument = "1";
        /// <summary>
        /// 时间戳超时
        /// </summary>
        internal const string Timestamp_Timeout = "1";
        /// <summary>
        /// 请求无效
        /// </summary>
        internal const string BadRequest = "1";
        
    }
}