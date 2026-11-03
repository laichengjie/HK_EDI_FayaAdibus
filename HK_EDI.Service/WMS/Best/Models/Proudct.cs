using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models
{
    [XmlType(TypeName = "product")]
    public class Proudct : IProductBase
    {
        //打上序列化标签
        [XmlElement(ElementName = "skuCode")]
        public string SkuCode { get; set; }

        [XmlArray(ElementName = "batchs")]
        public List<batch> batchs { get; set; }

        //[XmlElement(ElementName = "normalQuantity")]
        public string Qty 
        {
            get {
                return batchs.Sum(a => a.quantity).ToString();
            }
            set { }
        }
        
    }
     [XmlType("batch")]
    public class batch
    {
       [XmlElement(ElementName = "quantity")]
        public int quantity { get; set; }
    }
}
