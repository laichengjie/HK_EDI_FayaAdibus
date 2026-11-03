using HK_EDI.Service.WMS.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.SKU360.Models
{
   public class Response //: ResponseBase
    {
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
