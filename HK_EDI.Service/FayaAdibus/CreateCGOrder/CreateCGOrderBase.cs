using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.CreateCGOrder
{
    public class CreateCGOrderBase
    {
        public string CreateDate { get; set; }
        public string CustomerCode { get; set; }
        public string STC { get; set; }
        public string SalesOrder { get; set; }
        public string OrderType { get; set; }
        public string DeliveryNo { get; set; }
        public string Status { get; set; }
        public string ArticleNo { get; set; }
        public string Size { get; set; }
        public int QTY { get; set; }
        public float NetValue { get; set; }
        public float NetValueWithVAT { get; set; }
        public string Plant { get; set; }
        public string EAN { get; set; }
        public string OTB_Season { get; set; }
    }
    public class GetEstimatedShipmentBaseDetailItem
    {
        /// <summary>
        /// 耐克公司发货单单号，此字段值是判断是否对接到货单唯一值
        /// </summary>
        public string packlist_number { get; set; }
        /// <summary>
        /// 对应EPR货号  取SKU No(源文件格式为xxxxxx-xxx) ERP取值去掉其中的“-”，格式为xxxxxxxxx
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// 数量 （预留字段）
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// 外部订单编号
        /// </summary>
        public string extern_order_key { get; set; }
        /// <summary>
        /// 订单组
        /// </summary>
        public string order_group { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string order_type { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string buyer_po { get; set; }
        /// <summary>
        /// 发货仓库代码
        /// </summary>
        public string wh_code { get; set; }
        /// <summary>
        /// 预期发货日期（北京时间）
        /// </summary>
        public string etd { get; set; }
        /// <summary>
        /// 预期达到日期（北京时间）
        /// </summary>
        public string eta { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string customer_code { get; set; }
        /// <summary>
        ///客户名称
        /// </summary>
        public string customer_name { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string customer_address { get; set; }
        /// <summary>
        /// 箱子序号
        /// </summary>
        public string carton_number { get; set; }
        /// <summary>
        /// GPC
        /// </summary>
        public string gpc { get; set; }
        /// <summary>
        /// 样式代码
        /// </summary>
        public string style { get; set; }
        /// <summary>
        /// 颜色代码
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string dimension { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string quality { get; set; }
        /// <summary>
        /// 度量单位
        /// </summary>
        public string uom { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 货品UPC编号
        /// </summary>
        public string upc { get; set; }
        /// <summary>
        /// 发货数量（最终实际发货数量）
        /// </summary>
        public int qty_shipped { get; set; }
        /// <summary>
        /// 箱号（箱子唯一标识）
        /// </summary>
        public string label_number { get; set; }
        /// <summary>
        /// NIKE订单号
        /// </summary>
        public string nike_order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mark_for_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mark_for_name { get; set; }
        /// <summary>
        /// 业务单元分类（10，20，30）
        /// </summary>
        public string division { get; set; }
        /// <summary>
        /// 箱重量
        /// </summary>
        public decimal weight { get; set; }
        /// <summary>
        /// 箱体积
        /// </summary>
        public decimal volume { get; set; }
    }


}
