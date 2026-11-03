using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.HK
{
    [XmlRoot("Response")]
    public class StockQueryResponse
    {
        [XmlArray("Items")]
        public List<Item> items;
        public StockQueryResponse()
        {
            items = new List<Item>();
        }
        public int Status { get; set; }

        public string Reason { get; set; }
    }
    public class Item
    {
        [XmlElement(ElementName = "Qty")]
        public int Qty { get; set; }

        [XmlElement(ElementName = "SkuCode")]
        public string BarCode { get; set; }
    }
}
