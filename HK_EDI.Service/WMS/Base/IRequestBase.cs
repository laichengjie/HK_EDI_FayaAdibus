using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS
{
    /// <summary>
    /// 实现该接口 打上接口序列化字段标签
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRequestBase<T> where T : IProductBase
    {
          List<T> Products { get; set; }
          string ExpressNo { get; set; }
          string ExpressCode { get; set; }
         // string OrderCode { get; set; }
    }
}
