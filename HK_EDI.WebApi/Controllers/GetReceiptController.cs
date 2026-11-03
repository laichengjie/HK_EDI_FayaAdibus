using HK_EDI.Core.Common;
using HK_EDI.Service.FayaAdibus.Pur_ReceiptData;
using HK_EDI.Service.WMS.SKU360;
using HK_EDI.Services.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace HK_EDI.WebApi.Controllers
{

    public class GetReceiptController : ApiController
    {
        private readonly IReceiptDataApi _iReceiptDataApi;
        private readonly ILogger _logger;

        public GetReceiptController(IReceiptDataApi iReceiptDataApi,
       ILogger iLogger)
        {
            this._iReceiptDataApi = iReceiptDataApi;
            this._logger = iLogger;
        }
        public GetReceiptController()
        {

        }

        [HttpPost]
        [System.Web.Mvc.ValidateInput(false)]
        public async Task<HttpResponseMessage> GetReceiptData()
        {
            try
            {
                var formHeaders = Request.Headers;
                var formdata = Request.Content.ReadAsStringAsync().Result;

                Response result = new Response();
                HttpResponseMessage ss = new HttpResponseMessage();
                ss.Content = new ObjectContent(typeof(Response), result, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
              
                if (!formHeaders.Contains("Authorization"))
                {
                    result.error_code = "900";
                    result.error_message = "认证信息不存在";
                    ss.StatusCode = HttpStatusCode.Unauthorized;
                    return ss;
                }

                else
                {
                    string ReceiveKey = HttpUtility.UrlDecode(formHeaders.GetValues("Authorization").FirstOrDefault());//接收的密文
                    string Content = System.Web.HttpUtility.UrlDecode(formdata.ToString(), Encoding.UTF8).Trim();//接收的数据
                    string SignKey = System.Configuration.ConfigurationManager.AppSettings["SignKey"];
                    string valBase64 = EncodeBase64("utf-8", formdata.ToString());
                    string mikey = SHA1_Encrypt(valBase64 + SignKey);

                    if (string.IsNullOrEmpty(Content.Trim()))
                    {
                        result.error_code = "900";
                        result.error_message = "数据为空";
                        ss.StatusCode = HttpStatusCode.BadRequest;
                        return ss;
                    }
                    if (mikey != ReceiveKey)
                    {
                        result.error_code = "900";
                        result.error_message = "验证不通过";
                        ss.StatusCode = HttpStatusCode.BadRequest;
                        return ss;
                    }

                    //_iReceiptDataApi.GetReceiptDataSync(Content.Trim(), ref result);
                    if (!string.IsNullOrEmpty(result.error_code))
                    {
                        ss.StatusCode = HttpStatusCode.BadRequest;
                        return ss;
                    }

                }
                ss.StatusCode = HttpStatusCode.OK;
                return await Task.Run(() => ss);
            }
            catch (Exception ex)
            {
                _logger.Error("ReceiptDataApi - err:接口处理失败:" + ex.Message);
                Response result = new Response();
                result.error_code = "900";
                result.error_message = "接口处理失败:" + ex.Message;
                HttpResponseMessage ss = new HttpResponseMessage();
                ss.Content = new ObjectContent(typeof(Response), result, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                ss.StatusCode = HttpStatusCode.BadRequest;
                return ss;
            }
        }
        /// <summary>
        /// BASE64编码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="Source_String"></param>
        /// <returns></returns>
        public static string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }
    }


}