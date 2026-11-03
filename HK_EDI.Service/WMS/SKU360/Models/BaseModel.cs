using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class BaseModel
    {
        /// <summary>
        /// 单据类型
        /// </summary>
        [JsonIgnore]
        public string BillTypeID { get; set; }

        [JsonIgnore]
        public string CompanyID { get; set; }

        /// <summary>
        /// 平台代码
        /// </summary>
        [JsonProperty(PropertyName = "platform_code")]
        public int PlatformCode { get { return 2; } }
    }
}
