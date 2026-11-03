using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.GetEstimatedShipment
{
    public class GetEstimatedShipmentBase
    {
        public List<GetPackingListBaseHEAD> HEAD { get; set; }
        public List<GetEstimatedShipmentBODY> BODY { get; set; }
    }
    public class GetPackingListBaseHEAD
    {
        public string StatusCode { get; set; }
        public string MSG { get; set; }
    }
    public class GetEstimatedShipmentBODY
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty(PropertyName = "CREATE_DATE")]
        public string CreateDate { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        [JsonProperty(PropertyName = "Customer_NO")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 送货地址编号
        /// </summary>
        [JsonProperty(PropertyName = "Ship_To_No")]
        public string STC { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [JsonProperty(PropertyName = "SO_DOC_NO")]
        public string SalesOrder { get; set; }
        /// <summary>
        /// 订单类型，per order表示期货，stock order表示现货
        /// </summary>
        [JsonProperty(PropertyName = "ORDER_TYPE")]
        public string OrderType { get; set; }
        /// <summary>
        /// 预计发货单号
        /// </summary>
        [JsonProperty(PropertyName = "DELIVERY_NO")]
        public string DeliveryNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty(PropertyName = "CREDIT_STATUS")]
        public string Status { get; set; }
        /// <summary>
        /// 货号
        /// </summary>
        [JsonProperty(PropertyName = "MATERIAL_NO")]
        public string ArticleNo { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [JsonProperty(PropertyName = "SIZE_DESCRIPTION")]
        public string Size { get; set; }
        /// <summary>
        /// 预计发货数量
        /// </summary>
        [JsonProperty(PropertyName = "DELIVERYED_QTY")]
        public string QTY { get; set; }
        /// <summary>
        /// 预计不含税发货金额
        /// </summary>
        [JsonProperty(PropertyName = "net_value")]
        public float NetValue { get; set; }
        /// <summary>
        /// 预计含税发货金额
        /// </summary>
        [JsonProperty(PropertyName = "net_value_vat")]
        public float NetValueWithVAT { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [JsonProperty(PropertyName = "PLANT")]
        public string Plant { get; set; }
        /// <summary>
        /// 商品条码
        /// </summary>
        [JsonProperty(PropertyName = "EAN_No")]
        public string EAN { get; set; }
        /// <summary>
        /// OTB_Season
        /// </summary>
        [JsonProperty(PropertyName = "OTB_Season")]
        public string OTB_Season { get; set; }
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
        public string GetDate { get; set; }
        public string SoldToCode { get; set; }
    }

}
