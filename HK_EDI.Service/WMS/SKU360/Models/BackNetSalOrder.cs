using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
   public  class BackNetSalOrder
    {
        #region 公开属性

        [JsonProperty(PropertyName = "orders")]
        public List<BackNetOrder> BackNetOrder { get; set; }

        #endregion
        public BackNetSalOrder()
        {
            BackNetOrder = new List<BackNetOrder>();
        }
    }

    public class BackNetOrder
    {
        #region 公开属性
        /// <summary>
        /// 物流单号
        /// </summary>
        [JsonProperty(PropertyName = "order_code")]
        public string OrderCode { get; set; }
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
        ///退货物流单号
        /// </summary>
        [JsonProperty(PropertyName = "express_no")]
        public string ReturnsFreBillNo { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        [JsonProperty(PropertyName = "effective_time")]
        [JsonConverter(typeof(Utils.ChinaDateTimeConverter))]
        public DateTime BillDate { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [JsonProperty(PropertyName = "operator_name")]
        public string Operator { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "memo")]
        public string Remark { get; set; }
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

        /// <summary>
        /// 用端口号替代标记平台代码
        /// </summary>
        [JsonProperty(PropertyName = "platform_code")]
        public string FTPPort { get; set; }


        /// <summary>
        /// 发件人姓名
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string ConsigneeName { get; set; }
        /// <summary>
        /// 发件人手机
        /// </summary>
        [JsonProperty(PropertyName = "mobile")]
        public string TelPhone { get; set; }

        /// <summary>
        /// 发件人省份
        /// </summary>
        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// 发件人市
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// 发件人地址
        /// </summary>
        [JsonProperty(PropertyName = "detailAddress")]
        public string Address { get; set; }
        /// <summary>
        /// 关联原单号
        /// </summary>
        [JsonProperty(PropertyName = "return_orderno")]
        public string SourceBillNo { get; set; }

        [JsonProperty(PropertyName = "skus")]
        public List<BackOrderDetail> BackOrderDetail { get; set; }
        #endregion

        public BackNetOrder()
        {
            BackOrderDetail = new List<BackOrderDetail>();
        }

    }
}
