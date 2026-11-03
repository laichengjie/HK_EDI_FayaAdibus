using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HK_EDI.Service.WMS.ChunFeng
{
   public class RequestBase
    {
       /// <summary>
       /// 参数方法
       /// </summary>
       public string Method { get; set; }
       /// <summary>
       /// 货主代码
       /// </summary>
       public string Owner { get; set; }
       /// <summary>
       /// 请求时间
       /// </summary>
       public string RequestDate { get; set; }
       /// <summary>
       /// 签名
       /// </summary>
       public string Sign { get; set; }
       /// <summary>
       /// 请求数据
       /// </summary>
       public string RequestData { get; set; }

       /// <summary>
       /// 密钥
       /// </summary>
       public string AppKey { get; set; }
       public override string ToString()
       {
           string parmaeter = "method={0}&datetime={1}&sign={2}&data={3}";
           parmaeter = string.Format(parmaeter, Method,RequestDate,Sign,RequestData);
           return parmaeter;
       }

       public RequestBase()
       {

       }
       public RequestBase(string Method, string Owner, string RequestData,string AppKey)
       {
           this.Method = Method;
           this.Owner = Owner;
           this.RequestDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
           this.AppKey = AppKey;
           this.Sign = SignData(this.Owner,this.AppKey,this.RequestDate);
           this.RequestData = HttpUtility.UrlEncode(RequestData);
           
       }

       private static string SignData(string Owner,string AppKey,string RequestDate)
       {
           var sign = MD5Encrypt(Owner + AppKey + RequestDate).ToUpper();
           return sign;
       }

       ///   <summary>
       ///   给一个字符串进行MD5加密
       ///   </summary>
       ///   <param   name="strText">待加密字符串</param>
       ///   <returns>加密后的字符串</returns>
       public static string MD5Encrypt(string strText)
       {
          // MD5 md5 = new MD5CryptoServiceProvider();
          //// byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
          // byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
          // return System.Text.Encoding.Default.GetString(result);
           //byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(strText);
           //System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
           //byte[] result = md5.ComputeHash(data);
           //return Convert.ToBase64String(result);

           string pwd = "";
           MD5 md5 = MD5.Create();//实例化一个md5对像
           // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
           byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(strText.Trim()));
           // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
           for (int i = 0; i < s.Length; i++)
           {
               // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

               pwd = pwd + s[i].ToString("X2");

           }
           return pwd;
       }
    }
}
