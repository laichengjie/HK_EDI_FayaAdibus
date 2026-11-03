using HK_EDI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.XmlNodeSCM
{
    public class SCMMaster : BaseEntity
    {
        public string BillNo { get; set; }
        public string CompanyID { get; set; }
        public string BillTypeID { get; set; }
    }
}
