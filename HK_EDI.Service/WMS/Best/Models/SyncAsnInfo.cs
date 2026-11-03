using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class SyncAsnInfo : SyncBaseModel
    {
        public string asnCode { get; set; }
        public string actionType { get; set; }
        public bool udfFlag { get; set; }
        public string udf5 { get; set; }
        /// <summary>
        /// 传ERP单号
        /// </summary>
        public string refOrderCode { get; set; }
        public List<item> items { get; set; }
        public sender sender { get; set; }
        public SyncAsnInfo()
        {
            items = new List<item>();
            sender = new sender();
        }
    }

  

}
