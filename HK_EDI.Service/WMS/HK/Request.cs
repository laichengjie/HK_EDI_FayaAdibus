using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.HK
{
    /// <summary>
    /// 物流调恒康接口时的请求内容
    /// </summary>
    [XmlRoot("OrderStatus")]
    public class Request : IRequestBase<Product>
    {
        public string OrderCode { get; set; }

        [XmlArray("Items")]
        public List<Product> Products
        {
            get;
            set;
        }

        public string ExpressNo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ExpressCode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
