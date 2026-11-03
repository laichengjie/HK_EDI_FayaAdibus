using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class RequestContent : IRequestBase<Proudct>
    {
        //打上序列化标签
        [XmlArray("products")]
        public List<Proudct> Products { get; set; }

       
        [XmlElement("weight")]
        // 包裹重量
        public decimal Weight { get; set; }
        [XmlElement("shippingOrderNo")]
        public string ExpressNo { get; set; }
        [XmlElement("logisticsProviderCode")]
        public string ExpressCode { get; set; }
        
    }
}
