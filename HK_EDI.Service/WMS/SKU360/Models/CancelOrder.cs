using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
    public class CancelOrder
    {
        [JsonProperty(PropertyName = "order_code")]
        public string SignCode { get; set; }
    }
    public class CancelPurchaseOrder
    {
        [JsonProperty(PropertyName = "purchase_code")]
        public string SignCode { get; set; }
    }
}
