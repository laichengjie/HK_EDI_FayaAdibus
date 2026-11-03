using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.HK
{
    [XmlType("Item")]
    public class Product : IProductBase
    {
        [XmlElement(ElementName = "SkuCode")]
        public string SkuCode
        {
            get;
            set;
        }
        [XmlElement(ElementName = "Qty")]
        public string Qty
        {
            get;
            set;
        }
    }
}
