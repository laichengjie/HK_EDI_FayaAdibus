using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.SKU360.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    [XmlType(TypeName = "Item")]
    public class ProductBase
    {
        /// <summary>
        /// 货号+尺码
        /// </summary>
        [JsonProperty(PropertyName="product_code")]
        [XmlElement(ElementName = "SkuCode")]
        public string SkuCode { get; set; }

        [JsonIgnore]
        //[XmlAttribute(AttributeName = "Qty")]
        [XmlElement(ElementName = "Qty")]
        public decimal Qty { get; set; }

        [JsonIgnore]
        public string OSInterfaceID { get; set; }
        public string lotNumber { get; set; }
        public string uniqueId { get; set; }
    }
}
