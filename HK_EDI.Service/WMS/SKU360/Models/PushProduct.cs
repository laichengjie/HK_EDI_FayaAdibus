using HK_EDI.Service.WMS.CommonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
    /// <summary>
    /// 推送商品数据
    /// </summary>
    public class PushProduct
    {
        [JsonProperty(PropertyName = "products")]
        public List<Product> Products { get; set; }

        public PushProduct()
        {
            Products = new List<Product>();
        }
    }

    public class Product : ProductBase
    {
        #region 公开属性

        /// <summary>
        /// 货品档案修改时间
        /// </summary>
        [JsonProperty(PropertyName = "update_time")]
        public string ProductModifyDate { get; set; }
        /// <summary>
        /// 货品简称
        /// </summary>
        [JsonProperty(PropertyName = "product_name")]
        public string MaterialShortName { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        [JsonProperty(PropertyName = "bar_code")]
        public string BarCode { get; set; }
        /// <summary>
        /// 货号
        /// </summary>
        [JsonProperty(PropertyName = "material_code")]
        public string MaterialID { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [JsonProperty(PropertyName = "card_name")]
        public string CardName { get; set; }
        /// <summary>
        /// 吊牌价
        /// </summary>
        [JsonProperty(PropertyName = "product_price")]
        public string ProRetailPrice { get; set; }
        /// <summary>
        /// 颜色名称
        /// </summary>
        [JsonProperty(PropertyName = "product_color")]
        public string ColorName { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [JsonProperty(PropertyName = "product_kind")]
        public string KindName { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        [JsonProperty(PropertyName = "style_code")]
        public string StyleCode { get; set; }

        /// <summary>
        /// 尺码
        /// </summary>
        [JsonProperty(PropertyName = "product_size")]
        public string SizeName { get; set; }

        /// <summary>
        /// 季节
        /// </summary>
        [JsonProperty(PropertyName = "product_season")]
        public string SeasonName { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [JsonProperty(PropertyName = "product_year")]
        public int YearNo { get; set; }
        #endregion
    }


}
