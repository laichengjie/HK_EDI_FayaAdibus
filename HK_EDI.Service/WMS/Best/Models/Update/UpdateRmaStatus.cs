using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models.Update
{
   public class UpdateRmaStatus
    {

        [XmlElement(ElementName = "rmaCode")]
        public string OrderCode { get; set; }

        [XmlArray("products")]
        public List<Product> Products { get; set; }
    }
}
