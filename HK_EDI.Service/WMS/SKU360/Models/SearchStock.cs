using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
    class SearchStock
    {
        [JsonProperty(PropertyName = "page_no")]
        public int PageNo { get; set; }
        
        [JsonProperty(PropertyName = "page_size")]
        public int PageSize { get; set; }
        
        [JsonProperty(PropertyName = "product_code")]
        public string ProductCode { get; set; }

    }
}
