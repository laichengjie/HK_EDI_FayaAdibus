using HK_EDI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HK_EDI.Service.WMS.Best
{
    public static class Utils
    {
        /// <summary>
        /// 转为md5加密的字符串
        /// </summary>
        /// <returns></returns>
        public static string Convert2HexMD5(string str, Encoding encoding)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            encoding = encoding ?? Encoding.Default;
            byte[] byteValue = encoding.GetBytes(str);
            byte[] byteHash = md5.ComputeHash(byteValue);
            md5.Clear();

            string value = "";
            for (int i = 0; i < byteHash.Length; i++)
            {
                string hex = Convert.ToString(byteHash[i], 16);
                if (hex.Length < 2) hex = hex.PadLeft(2, '0');
                value += hex;//Convert.ToString(byteHash[i], 16); //bytHash[i].ToString("x2");
            }

            //return value;

            //MD5 m = new MD5CryptoServiceProvider();
            //byte[] b = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str));
            //String output = "";
            //string a = "";
            //for (int i = 0; i < b.Length; i++)
            //{
            //    a = Convert.ToString(b[i], 16);
            //    if (a.Length < 2)
            //    {
            //        a = "0" + a;
            //    }
            //    output = output + a;
            //}
            //return output;

            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str));
            StringBuilder ret = new StringBuilder();
            foreach (var c in s)
            {
                string v = Convert.ToString(c, 16);
                if (v.Length == 1)
                {
                    ret.Append("0");
                }
                ret.Append(v);
            }
            return ret.ToString();


        }


        /// <summary>
        /// 百世反序列化 。。。。。。。。。。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static T Serialize4Best<T>(string objData, Encoding encoding)
        {
            //匹配整个根节点
            string pattern = @"(?<RootNode><[^>?]+>)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(objData);
            if (!match.Success) throw new Exception("响应的内容格式不正确。", new Exception(objData));
            string rootNode = match.Groups["RootNode"].Value;

            //替换根节点，去掉没用的命名空间
            objData = regex.Replace(objData, string.Format("<{0}>", typeof(T).Name), 1);

            //匹配整个根节点的名称，包含命名空间
            pattern = @"<(?<RootName>[^\s]+)\s+";
            regex = new Regex(pattern);
            match = regex.Match(rootNode);
            if (!match.Success) throw new Exception("百世响应的内容格式不正确。", new Exception(rootNode));
            string rootName = match.Groups["RootName"].Value;

            //替换根节点名称
            objData = objData.Replace(rootName, typeof(T).Name);
            return XmlHelper.XmlDeserialize<T>(objData, encoding);
        }


        private static readonly Type[] WriteTypes = new[] {
        typeof(string), typeof(DateTime), typeof(Enum), 
        typeof(decimal), typeof(Guid),
        };
        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive || WriteTypes.Contains(type);
        }
        public static XElement ToXml(this object input)
        {
            return input.ToXml(null);
        }
        public static XElement ToXml(this object input, string element)
        {
            if (input == null)
                return null;

            if (string.IsNullOrEmpty(element))
                element = "object";
            element = XmlConvert.EncodeName(element);
            var ret = new XElement(element);

            if (input != null)
            {
                var type = input.GetType();
                var props = type.GetProperties();

                var elements = from prop in props
                               let name = XmlConvert.EncodeName(prop.Name)
                               let val = prop.GetValue(input, null)
                               let value = prop.PropertyType.IsSimpleType()
                                    ? new XElement(name, val)
                                    : val.ToXml(name)
                               where value != null
                               select value;

                ret.Add(elements);
            }

            return ret;
        }
    }
}
