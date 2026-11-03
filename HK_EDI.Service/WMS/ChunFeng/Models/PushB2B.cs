using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.ChunFeng.Models
{
    /// <summary>
    /// 推送调拨单
    /// </summary>
   public class PushB2B
    {
        #region 公开属性

       [JsonProperty(PropertyName = "orders")]
       public List<B2BOrders> Orders { get; set; }

       #endregion

        public PushB2B()
        {

            Orders = new List<B2BOrders>();
        }
    }


   public class B2BOrders : BaseModel
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
        /// <summary>
        /// 收货对象编号
        /// </summary>
        [JsonProperty(PropertyName = "purchaser_no")]
        public string VendCustCode { get; set; }
        /// <summary>
        /// 收货对象名称
        /// </summary>
        [JsonProperty(PropertyName = "purchaser_name")]
        public string VendCustName { get; set; }
        /// <summary>
        /// 业务地区
        /// </summary>
        [JsonProperty(PropertyName = "business_area ")]
        public string Area { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [JsonProperty(PropertyName = "rec_province")]
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [JsonProperty(PropertyName = "rec_city")]
        public string City { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [JsonProperty(PropertyName = "rec_address")]
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [JsonProperty(PropertyName = "rec_name")]
        public string ConsigneeName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [JsonProperty(PropertyName = "rec_mobile")]
        public string TelPhone { get; set; }
        
       [JsonProperty(PropertyName = "skus")]
        public List<B2BOrdersDetail> OrdersDetail { get; set; }

       /// <summary>
       /// 调出平台代码
       /// </summary>
       [JsonProperty(PropertyName = "fromplatform_code")]
       public string FromplatformCode { get; set; }

       /// <summary>
       /// 调入平台代码
       /// </summary>
       [JsonProperty(PropertyName = "toplatform_code")]
       public string ToplatformCode { get; set; }

        /// <summary>
        /// 线下值：2,电商线上值：1
        /// </summary>
        [JsonProperty(PropertyName = "platform_code")]
        public string FTPPort { get; set; }
        /// <summary>
        /// 承运商代码
        /// </summary>
        [JsonProperty(PropertyName = "express")]
       public string ExpressCode { get { return "SW"; } }
       #endregion

        public B2BOrders()
       {
           OrdersDetail = new List<B2BOrdersDetail>();
       }
   }
   /// <summary>
   /// 明细
   /// </summary>
   public class B2BOrdersDetail
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
