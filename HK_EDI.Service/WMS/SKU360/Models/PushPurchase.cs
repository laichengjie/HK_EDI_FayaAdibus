using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
    /// <summary>
    /// 推送采购单(入库单)
    /// </summary>
    public class PushPurchase 
    {
        #region 公开属性
        [JsonProperty(PropertyName = "purchases")]
        public List<Purchases> Purchases { get; set; }
        #endregion

        public PushPurchase()
        {
            Purchases = new List<Purchases>();
        }
    }


    public class Purchases : BaseModel
    {
        #region 公开属性
        /// <summary>
        /// 物流单号
        /// </summary>
        [JsonProperty(PropertyName = "purchase_code")]
        public string Purchasecode { get; set; }

        /// <summary>
        /// 收货类型
        /// </summary>
        [JsonProperty(PropertyName = "purchase_typename")]
        public string TranType { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        [JsonProperty(PropertyName = "back_order")]
        public string BillNo { get; set; }
        
        /// <summary>
        /// 生效时间
        /// </summary>
        [JsonProperty(PropertyName = "effective_time")]
        [JsonConverter(typeof(HK_EDI.Service.WMS.SKU360.Utils.ChinaDateTimeConverter))]
        public DateTime BillDate { get; set; }
       
        /// <summary>
        /// 操作员
        /// </summary>
         [JsonProperty(PropertyName = "operator_name")]
        public string Operator { get; set; }
        /// <summary>
        /// 来货方编号
        /// </summary>
         [JsonProperty(PropertyName = "supplier_no")]
        public string VendCustCode { get; set; }
        /// <summary>
        /// 来货方简称
        /// </summary>
        [JsonProperty(PropertyName = "supplier_sortname")]
        public string VendCustName { get; set; }
        /// <summary>
        /// 仓库代码
        /// </summary>
        [JsonProperty(PropertyName = "stock_code")]
        public string StockID { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [JsonProperty(PropertyName = "stock_name")]
        public string StockName { get; set; }
        /// <summary>
        /// 业务地区
        /// </summary>
        [JsonProperty(PropertyName = "business_area")]
        public string Area { get; set; }

        [JsonProperty(PropertyName = "rec_county")]
        public string Country { get; set; }

        /// <summary>
        /// 线下值：2,电商线上值：1
        /// </summary>
        [JsonProperty(PropertyName = "platform_code")]
        public string FTPPort { get; set; }

        [JsonProperty(PropertyName = "skus")]
        public List<PurchaseDetail> PushPurchaseDetail { get; set; }

        #endregion
        public Purchases()
        {
            PushPurchaseDetail = new List<PurchaseDetail>();
        }
    }

    /// <summary>
    /// 明细
    /// </summary>
    public class PurchaseDetail
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
