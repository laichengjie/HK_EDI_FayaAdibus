using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Core.Infrastructure;
using HK_EDI.Services.Authentication;
using HK_EDI.Services.Tasks;
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace HK_EDI.Web.Filter
{
    /// <summary>
    /// 检查网站是否在授权时间内
    /// </summary>
    public class CheckAuthorizedTimeFilter : IAuthorizationFilter
    {
        private readonly GolbalSetting _golbalSetting;
        //DES的IV向量
        private readonly byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //标志是否已禁用了网站功能。
        private bool Flag;
        //用来存放网站的授权时间
        private DateTime _authorizedTime;

        public CheckAuthorizedTimeFilter(GolbalSetting golbalSettingg)
        {
            _golbalSetting = golbalSettingg;
            InitAuthorizedTime();
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Flag)//如果已禁用了网站就不必再停止计划任务，和登出用户了
            {
                //重定向到登陆页面
                RedirectToLogin(filterContext);
            }
            else if (DateTime.Now.CompareTo(_authorizedTime) > 0)
            {
                //停止计划任务
                TaskManager.Instance.Stop();
                //登出用户
                EngineContext.Current.Resolve<IAuthenticationService>().SignOut();
                Flag = true;
                //重定向到登陆页面
                RedirectToLogin(filterContext);
            }
            else if ((_authorizedTime - DateTime.Now).TotalSeconds < 1296000)
            {
                //距离授权到期已小于15天
                if (string.IsNullOrWhiteSpace(_golbalSetting.AuthorizedHintFormat))
                {
                    _golbalSetting.AuthorizedHintFormat = "此功能在{0}到期，为了避免影响正常使用，请尽快激活.";
                }
                filterContext.RouteData.DataTokens["ShowAuthorizedHint"] = string.Format(_golbalSetting.AuthorizedHintFormat, _authorizedTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            //大于15天不做任何操作
        }

        /// <summary>
        /// Des解密
        /// <param name="encryptString">待解密的字符串</param>
        /// <param name="encryptKey">密钥,要求为8位</param>
        /// </summary>
        private string DecryptDES(string decryptString, string decryptKey)
        {
            if (decryptString == null)
            {
                return null;
            }
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = IV;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">密钥,要求为8位</param>
        private string EncryptDES(string encryptString, string encryptKey)
        {
            if (encryptString == null)
            {
                return null;
            }
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = IV;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 初始化网站授权时间
        /// </summary>
        private void InitAuthorizedTime()
        {
            var timeStr = DecryptDES(_golbalSetting.AuthorizedTime, ConfigurationManager.AppSettings["EncryptKey"]);
            if (!DateTime.TryParse(timeStr, out _authorizedTime))
            {
                _authorizedTime = new DateTime(2058, 1, 1);
            }
        }

        /// <summary>
        /// 将网站重定向到登陆页
        /// </summary>
        private void RedirectToLogin(AuthorizationContext filterContext)
        {
            //禁用登陆功能
            filterContext.RouteData.DataTokens["DisalbeLogin"] = _authorizedTime.ToString("yyyy-MM-dd HH:mm:ss");
            if (!filterContext.RouteData.Values["controller"].Equals("Customer") || !filterContext.RouteData.Values["action"].Equals("Login"))
            {
                filterContext.Result = new RedirectToRouteResult("Login", null);
            }
        }
    }
}