using HK_EDI.Core;
using HK_EDI.Core.Infrastructure;
using HK_EDI.WebApi.Exceptions;
using HK_EDI.Services.Authentication;
using HK_EDI.Services.Customers;
using HK_EDI.Services.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HK_EDI.WebApi.Security
{
    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var _httpRequestBase = EngineContext.Current.Resolve<HttpRequestBase>();
            string token = _httpRequestBase.Params.AllKeys.Contains("token") ?
                _httpRequestBase.Params["token"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(token))
                throw new NotAuthorizeException();
            if (CheckToken(token))
            {
                var permissionService = EngineContext.Current.Resolve<IPermissionService>();
                if (!permissionService.Authorize(StandardPermissionProvider.AccessWebApiService))
                    throw new NotAuthorizeException();
            }
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token">安全令牌</param>
        /// <returns></returns>
        private bool CheckToken(string token)
        {
            string[] ckeckList = token.Split('|');
            if (ckeckList.Length != 3)
            {
                return false;
            }
            DateTime timestamp = DateTime.MinValue;
            DateTime.TryParseExact(ckeckList[1], "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None, out timestamp);
            if (timestamp < DateTime.Now.AddMinutes(-30))
            {
                throw new TimestampTimeoutException();
            }
            string userName = ckeckList[0];
            var _customerService = EngineContext.Current.Resolve<ICustomerService>();
            var customer = _customerService.GetCustomerByEmail(userName);
            if (customer == null)
                return false;
            string DM5First = Md5String(ckeckList[0] + customer.Password + ckeckList[1]);//编号+密码+时间 MD5加密
            string DM5Second = Md5String(DM5First + ckeckList[0]);//第一次加密+编号 再进行MD5加密
            if (DM5Second == ckeckList[2] || DM5Second.ToLower() == ckeckList[2])
            {
                var _workContext = EngineContext.Current.Resolve<IWorkContext>();
                _workContext.CurrentCustomer = customer;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取指定的字符串 MD5 后的值
        /// </summary>
        /// <param name="sourceString">MD5 的值</param>
        /// <returns></returns>
        private string Md5String(string sourceString)
        {
            string _Encrypassword = string.Empty;
            byte[] result = Encoding.Default.GetBytes(sourceString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            _Encrypassword = BitConverter.ToString(output).Replace("-", "");
            return _Encrypassword;
        }
    }
}