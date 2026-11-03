using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.ChunFeng.Models
{
    public class CancelOrder
    {
        [JsonProperty(PropertyName = "order_code")]
        public string SignCode { get; set; }
        /// <summary>
        /// 线下值：2,电商线上值：1
        /// </summary>
        [JsonProperty(PropertyName = "platform_code")]
        public string FTPPort { get; set; }
    }
    public class CancelPurchaseOrder
    {
        [JsonProperty(PropertyName = "purchase_code")]
        public string SignCode { get; set; }
        /// <summary>
        /// 线下值：2,电商线上值：1
        /// </summary>
        [JsonProperty(PropertyName = "platform_code")]
        public string FTPPort { get; set; }
    }
}
