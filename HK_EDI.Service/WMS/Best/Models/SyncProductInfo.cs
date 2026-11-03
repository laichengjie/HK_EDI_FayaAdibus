using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class SyncProductInfo : SyncBaseModel
    {
       
        public List<product> products { get; set; }

        public SyncProductInfo()
        {
            products = new List<product>();
        }
    }
    public class product
    {
        [XmlElementAttribute("skuCode")]
        public string BarCode { get; set; }
        public string actionType { get; set; }
        [XmlElementAttribute("name")]
        public string MaterialShortName { get; set; }

        /// <summary>
        /// 款号
        /// </summary>
        [XmlElementAttribute("unit")]
        public string StyleCode { get; set; }

        [XmlElementAttribute("packageSpec")]
        public string ColorName { get; set; }

        [XmlElementAttribute("property")]
        public string SizeName { get; set; }

        [XmlIgnoreAttribute]
        public string SkuCode { get; set; }

        [XmlIgnoreAttribute]
        public string OSInterfaceID { get; set; }
    }
}
