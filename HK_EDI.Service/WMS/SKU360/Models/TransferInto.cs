using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.SKU360.Models
{
   [XmlRoot("OrderStatus")]
   public class TransferInto
    {
        public string OrderCode { get; set; }
    }
}
