using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.ChunFeng.Models
{
     [XmlRoot("OrderStatus")]
     public class Response2
    {
         public string success { get; set; }
         public string reason { get; set; }
         public string msg { get; set; }

    }
}
