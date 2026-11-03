using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace HK_EDI.Service.Utility
{
   
    /// <summary>
    /// 
    /// </summary>
    [Browsable(true)]
    public class JsonHelper
    {
        /// <summary>
        /// 序列化object对象为UTF8编码的JSON字符串
        /// </summary>
        /// <param name="obj">输入的对象</param>
        /// <returns>返回的Json字符串</returns>
        public static string SerializeToJsonString(object obj)
        {
            string result = null;
            try
            {
                if (obj != null)
                {
                    var ser = new JavaScriptSerializer();
                    result = ser.Serialize(obj);
                }
            }
            catch (Exception innerException)
            {
                throw new Exception(string.Format("不能将对象：{0}，序列化为JSON字符串", obj.GetType().Name), innerException);
            }
            return result;
        }

        /// <summary>
        /// 反序列化JSON字符串为指定类型
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>反序列化后的结果对象</returns>
        public static T DeserializeFromJsonString<T>(string jsonString)
        {
            T result = default(T);
            try
            {
                var ser = new JavaScriptSerializer();
                result = ser.Deserialize<T>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("JSON字符串反序列化为对象出现异常，对象类型：{0}，JSON字符串：{1}", typeof(T).FullName, jsonString), ex);
            }
            return result;
        }
    }
}
