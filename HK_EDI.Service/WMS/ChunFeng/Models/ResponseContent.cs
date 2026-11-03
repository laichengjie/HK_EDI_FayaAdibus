using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HK_EDI.Service.WMS.ChunFeng.Models
{
     [XmlRoot("OrderStatus")]
     public class ResponseContent
    {
        /// <summary>
        /// 入库单编号
        /// </summary>
        public string orderCode { get; set; }
        /// <summary>
        /// 仓库代码
        /// </summary>
        public string warehouseCode { get; set; }
        /// <summary>
        /// 客户编号(客户号)
        /// </summary>
        public string customerId { get; set; }
        /// <summary>
        /// 入库类型：退货入库(Return)、调拨入库(Transfer)、采购入库(Normal)、其他入库(Others)
        /// </summary>
        public string receiptType { get; set; }
        /// <summary>
        /// 入库日期
        /// </summary>
        public string receiptDate { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string @operator { get; set; }
        /// <summary>
        /// 入库单备注
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string lot { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string MailNo { get; set; }
        /// <summary>
        /// 物流公司代码
        /// </summary>
        public string TmsCode { get; set; }
        public string OrderCode { get; set; }

        //[XmlElement(ElementName="Items")]
        [XmlArray("Items")]
        public List<ProductBase> Products { get; set; }

        public ResponseContent()
        {
            Products = new List<ProductBase>();
        }

    }
}
