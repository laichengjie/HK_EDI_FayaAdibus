using HK_EDI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.XmlNodeSCM
{
    public class SCMMatTransInMaster : BaseEntity
    {
        public string shNo { get; set; }
        public string designNo { get; set; }
        public string productCode { get; set; }

        public string hkOrderNo { get; set; }

        public string colorNo { get; set; }
        public string size { get; set; }
        public Nullable<decimal> inCount { get; set; }

        public Nullable<DateTime> inTime { get; set; }

    }
}
