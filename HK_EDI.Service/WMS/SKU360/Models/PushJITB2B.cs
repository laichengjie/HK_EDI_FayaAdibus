using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
    public class PushJITB2B
    {
        #region 公开属性

        [JsonProperty(PropertyName = "orders")]
        public List<JITB2BOrders> Orders { get; set; }

        #endregion

        public PushJITB2B()
        {

            Orders = new List<JITB2BOrders>();
        }
    }
    public class JITB2BOrders : BaseModel
    {
        #region 公开属性


        /// <summary>
        /// 物流单号
        /// </summary>
        [JsonProperty(PropertyName = "order_code")]
        public string OrderCode { get; set; }
        /// <summary>
        /// SKUTYPE
        /// </summary>
        [JsonProperty(PropertyName = "B2B_type")]
        public int SKUType { get; set; }
        /// <summary>
        /// 出货类型
        /// </summary>
        [JsonProperty(PropertyName = "B2B_name")]
        public string ShipType { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        [JsonProperty(PropertyName = "out_tid")]
        public string BillNo { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        [JsonProperty(PropertyName = "effective_time")]
        [JsonConverter(typeof(Utils.ChinaDateTimeConverter))]
        public DateTime BillDate { get; set; }
       
        [JsonProperty(PropertyName = "platform_code")]
        public string FTPPort { get; set; }
        /// <summary>
        /// 承运商代码
        /// </summary>
        [JsonProperty(PropertyName = "express")]
        public string ExpressCode { get; set; }


        /// <summary>
        /// 送货仓库
        /// </summary>
        [JsonProperty(PropertyName = "receiving_warehouse_name")]
        public string Warehouse { get; set; }
        /// <summary>
        /// 要求到货时间
        /// </summary>
        [JsonProperty(PropertyName = "appointment_date")]
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// 入库单号
        /// </summary>
        [JsonProperty(PropertyName = "appointment_no")]
        public string StorageNo { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        [JsonProperty(PropertyName = "appointment_brand_name")]
        public string BrandName { get; set; }

        [JsonProperty(PropertyName = "skus")]
        public List<JITB2BOrdersDetail> OrdersDetail { get; set; }
        #endregion
        public JITB2BOrders()
        {
            OrdersDetail = new List<JITB2BOrdersDetail>();
        }
    }
    /// <summary>
    /// 明细
    /// </summary>
    public class JITB2BOrdersDetail
    {
        /// <summary>
        /// 货号+尺码
        /// </summary>
        [JsonProperty(PropertyName = "product_code")]
        public string SKUCode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [JsonProperty(PropertyName = "qty")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
