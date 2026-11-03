using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using HK_EDI.Core.Infrastructure;
using Autofac;
using System.Linq;
using System.Collections;
using HK_EDI.Core.Common;

namespace HK_EDI.Core
{
    public static class CommonExtension
    {
        #region String Extension

        /// <summary>
        /// 处理Like查询中包含特殊字符
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string SqlQueryEscape(this string val)
        {
            return val.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]").Replace("^", "[^]").Replace("'", "''");
        }

        public static string[] Split(this string val, string code)
        {
            return Regex.Split(val, code);
        }

        public static bool IsNullOrEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }

        public static bool IsNotEmpty(this string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        public static bool IsNumeric(this string val)
        {
            if (val != null)
            {
                string str = val;
                if (str.Length > 0 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    return true;
                }
            }
            return false;
        }

        public static string TrimSpace(this string val)
        {
            if (val == null)
                return null;
            else
                return val.Trim();
        }

        public static bool Contains(this string val, string value, StringComparison comp)
        {
            return val.IndexOf(value, comp) >= 0;
        }

        public static DateTime? ToDateTime(this string val)
        {
            if (string.IsNullOrEmpty(val)) return null;
            DateTime dt;
            if (DateTime.TryParse(val, out dt))
            {
                return dt;
            }
            return null;
        }

        public static bool ToBoolean(this string val)
        {
            if (string.IsNullOrEmpty(val))
                return false;
            if (val == "1")
                return true;
            else if (val == "0")
                return false;
            bool result = false;
            bool.TryParse(val, out result);
            return result;
        }

        public static int ToInt(this string val)
        {
            int intValue;
            if (int.TryParse(val, out intValue))
            {
                return intValue;
            }
            return 0;
        }

        public static decimal ToDecimal(this string val)
        {
            decimal intValue;
            if (decimal.TryParse(val, out intValue))
            {
                return intValue;
            }
            return 0;
        }

        public static double ToDouble(this string val)
        {
            double result;
            if (double.TryParse(val, out result))
            {
                return result;
            }
            return 0;
        }

        public static float ToFloat(this string val)
        {
            float result;
            if (float.TryParse(val, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 按照长度截取字符串
        /// </summary>
        /// <param name="val"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Truncate(this string val, int length)
        {
            if (string.IsNullOrEmpty(val))
                return val;
            return (val.Length > length ? val.Substring(0, length - 1) : val);
        }

        /// <summary>
        /// 按照长度截取字符串
        /// </summary>
        /// <param name="val"></param>
        /// <param name="length"></param>
        /// <param name="coda">结尾符号</param>
        /// <returns></returns>
        public static string Truncate(this string val, int length, string coda)
        {
            if (string.IsNullOrEmpty(val))
                return val;
            return (val.Length > length ? val.Substring(0, length - 1) + coda : val);
        }

        public static int ParseDayOfWeek(this string val)
        {
            val = val.ToLower();
            switch (val)
            {
                case "monday":
                    return 1;
                case "tuesday":
                    return 2;
                case "wednesday":
                    return 3;
                case "thursday":
                    return 4;
                case "friday":
                    return 5;
                case "saturday":
                    return 6;
                case "sunday":
                    return 7;
                default:
                    return 0;
            }
        }

        #endregion

        #region Object Extension

        public static DateTime? ToDateTime(this object val)
        {
            if (val == null) return null;
            return val.ToString().ToDateTime();
        }

        public static bool ToBoolean(this object val)
        {
            if (val == null) return false;
            return val.ToString().ToBoolean();
        }

        public static int ToInt(this object val)
        {
            if (val == null) return 0;
            var type = val.GetType();
            if (type.Equals(typeof(decimal)) || type.Equals(typeof(double)) || type.Equals(typeof(float)) || type.Equals(typeof(long)))
            {
                return Convert.ToInt32(val);
            }
            return val.ToString().ToInt();
        }

        public static decimal ToDecimal(this object val)
        {
            if (val == null) return 0;
            return val.ToString().ToDecimal();
        }

        public static double ToDouble(this object val)
        {
            if (val == null) return 0;
            return val.ToString().ToDouble();
        }
        public static float ToFloat(this object val)
        {
            if (val == null) return 0;
            return val.ToString().ToFloat();
        }

        #endregion

        #region Numeral Extension
        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this decimal val, decimal start, decimal end)
        {
            return val >= start && val <= end;
        }
        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this int val, int start, int end)
        {
            return val >= start && val <= end;
        }
        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this float val, float start, float end)
        {
            return val >= start && val <= end;
        }
        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this double val, double start, double end)
        {
            return val >= start && val <= end;
        }

        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this decimal? val, decimal start, decimal end)
        {
            return val.HasValue ? (val >= start && val <= end) : false;
        }
        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this int? val, int start, int end)
        {
            return val.HasValue ? (val >= start && val <= end) : false;
        }
        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this float? val, float start, float end)
        {
            return val.HasValue ? (val >= start && val <= end) : false;
        }
        /// <summary>
        /// 是否在指定范围内
        /// </summary>
        /// <param name="start">起始数值</param>
        /// <param name="end">结束数值</param>
        public static bool Between(this double? val, double start, double end)
        {
            return val.HasValue ? (val >= start && val <= end) : false;
        }

        #endregion

        #region IEnumerable
        public static bool Exists<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                    return true;
            }
            return false;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TResult>
            (
                this IEnumerable<TOuter> outer,
                IEnumerable<TInner> inner,
                Func<TInner, TOuter, bool> predicate,
                Func<TOuter, TInner, TResult> resultSelector
            )
        {
            List<TResult> result = new List<TResult>();
            foreach (var outerItem in outer)
            {
                foreach (var innerItem in inner)
                {
                    if (predicate(innerItem, outerItem))
                    {
                        result.Add(resultSelector(outerItem, innerItem));
                    }
                }
            }
            return result;
        }

        #endregion

        public static string GetPropertyName<T>(this T obj, Expression<Func<T, object>> expr) where T : new()
        {
            var rtn = "";
            if (expr.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression)expr.Body).Type.Name;
            }
            return rtn;
        }

        public static T ChangeType<T>(this object obj, T t)
        {
            return (T)obj;
        }

        public static T CreateInstance<T>(this string type, ILifetimeScope scope)
        {
            object instance = null;
            var type2 = System.Type.GetType(type);
            if (type2 != null)
            {
                if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
                {
                    //not resolved
                    instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
                }
            }

            return (T)instance;
        }

        public static IEnumerable<BaseItem> ToEnumSelectList<TEnum>(this TEnum enumObj,
            bool markCurrentAsSelected = true, int[] valuesToExclude = null) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new BaseItem { ID = Convert.ToInt32(enumValue).ToString(), Name = GetEnumDispalyName(enumValue) };
            return values;
        }

        public static string GetEnumDispalyName(object em)
        {
            Type type = em.GetType();
            var fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;
            var attributes = fd.GetCustomAttributes(typeof(EnumResourceAttribute), false);
            if (attributes == null || attributes.Length == 0)
                return string.Empty;
            EnumResourceAttribute attr = attributes[0] as EnumResourceAttribute;
            return attr.DisplayName;
        }

    }
}
