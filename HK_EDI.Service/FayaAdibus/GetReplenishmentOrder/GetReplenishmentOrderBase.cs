using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.GetReplenishmentOrder
{
    public class GetReplenishmentOrderBase
    {
        public List<GetReplenishmentOrderBaseHEAD> HEAD { get; set; }
        public List<GetReplenishmentOrderBaseBODY> BODY { get; set; }
    }
    public class GetReplenishmentOrderBaseHEAD
    {
        public string StatusCode { get; set; }
        public string MSG { get; set; }
    }
    /// <summary>
    /// 补货单明细，字段按接口实际报文字段调整
    /// </summary>
    public class GetReplenishmentOrderBaseBODY
    {
        /// <summary>
        /// 补货单号
        /// </summary>
        public string ReplenishmentNo { get; set; }
        public string TelNo { get; set; }
        public string HU_No { get; set; }
        public string EAN { get; set; }
        public string Address { get; set; }
        public string CartonNo { get; set; }
        public string ETA { get; set; }
        public string Vender { get; set; }
        public string CartonWeight { get; set; }
        public string ItemSize { get; set; }
        /// <summary>
        /// 收货方/客户代码
        /// </summary>
        public string ShipTo { get; set; }
        public string Division { get; set; }
        public string Brand { get; set; }
        public string Contacts { get; set; }
        public string SoldTo { get; set; }
        [JsonProperty(PropertyName = "PN No")]
        public string PNNo { get; set; }
        public string ETD { get; set; }
        [JsonProperty(PropertyName = "SO No")]
        public string SONo { get; set; }
        public string ArticleNo { get; set; }
        public string Quantity { get; set; }
    }

    public class PostHeader
    {
        public string SERVICE_CODE { get; set; }
        public string CONSUMER_SYSCODE { get; set; }
        public string VERSION { get; set; }
        public string MESSAGE_ID { get; set; }

        public string DTSEND { get; set; }
    }

    public class PostDATA
    {
        public PostHEAD HEAD { get; set; }
    }
    public class PostHEAD
    {
        public string KAName { get; set; }
        public string GetDateTime { get; set; }
        public string CustomerCode { get; set; }
    }
}
