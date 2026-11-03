using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;

namespace HK_EDI.Service.Common
{
    public class SerializeHelper
    {
        /*
        /// <summary>
        /// 对象序列化成Json字符串
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="obj">要序列化的对象（对象需使用DataContract特性标记，属性需使用DataMember特性标记）</param>
        /// <returns></returns>
        public static string SerializeToJson<T>(T obj) where T : class
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, obj);
                string strJson = Encoding.UTF8.GetString(ms.ToArray());
                return strJson;
            }
        }

        /// <summary>
        /// Json字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strJson">Json字符串</param>
        /// <returns></returns>
        public static T DeserializeFromJson<T>(string strJson) where T : class
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(strJson)))
            {
                T obj = jsonSerializer.ReadObject(ms) as T;
                return obj;
            }
        }

        ///// <summary>
        ///// 对象序列化成 XML 字符串
        ///// </summary>
        ///// <typeparam name="T">T</typeparam>
        ///// <param name="obj">要序列化的对象</param>
        ///// <returns></returns>
        //public static string SerializeToToXml<T>(T obj) where T : class
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        xmlSerializer.Serialize(ms, obj);
        //        string strXml = Encoding.UTF8.GetString(ms.ToArray());
        //        return strXml;
        //    }
        //}

        /// <summary>
        /// 对象序列化成 XML 字符串
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string Serialize2Xml(object obj, Encoding encoding)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                string strXml = (encoding ?? Encoding.UTF8).GetString(ms.ToArray());
                return strXml;
            }
        }

        /// <summary>
        /// XML 字符串 反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static T DeserialFromXml<T>(string strXml) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(strXml)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    T obj = xmlSerializer.Deserialize(xmlReader) as T;
                    return obj;
                }
            }
        }

        /// <summary>
        /// DataContract　序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="encodeName"></param>
        /// <returns></returns>
        public static string DataContractSerialize(object obj, string encodeName = "utf-8")
        {
            Encoding encoding = Encoding.GetEncoding(encodeName) ?? Encoding.UTF8;
            string xml = string.Empty;
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                xml = encoding.GetString(ms.ToArray());
                return xml;
            }
        }

        /// <summary>
        /// DataContract　反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static T DataContractDeSerialize<T>(string objData, string encodeName = "utf-8")
        {
            Encoding encoding = Encoding.GetEncoding(encodeName) ?? Encoding.UTF8;
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            byte[] buffer = encoding.GetBytes(objData);
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                T value = (T)serializer.ReadObject(ms);
                return value;
            }
        }

        /// <summary>
        /// 百世DataContract　反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static T DataContractDeSerialize4Best<T>(string objData, string encodeName = "utf-8")
        {
            //匹配整个根节点
            string pattern = @"(?<RootNode><[^>?]+>)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(objData);
            if (!match.Success) throw new Exception("响应的内容格式不正确。", new Exception(objData));
            string rootNode = match.Groups["RootNode"].Value;

            //替换根节点，等于去掉百世的命名空间，加上HK的命名空间
            objData = regex.Replace(objData, string.Format(@"<{0} xmlns=""{1}"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">",
                typeof(T).Name, "http://schemas.datacontract.org/2004/07/" + typeof(T).Namespace), 1);

            //匹配整个根节点的名称，包含命名空间
            pattern = @"<(?<RootName>[^\s]+)\s+";
            regex = new Regex(pattern);
            match = regex.Match(rootNode);
            if (!match.Success) throw new Exception("百世响应的内容格式不正确。", new Exception(rootNode));
            string rootName = match.Groups["RootName"].Value;

            //替换根节点名称
            objData = objData.Replace(rootName, typeof(T).Name);
            return SerializeHelper.DataContractDeSerialize<T>(objData, encodeName);
        }

        */
    }
}
