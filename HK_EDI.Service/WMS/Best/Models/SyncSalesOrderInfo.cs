using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class SyncSalesOrderInfo : SyncBaseModel
    {
        public string orderCode { get; set; }
        public string actionType { get; set; }
        public string orderType { get; set; }

        /// <summary>
        /// 发票标记(是否需要发票)
        /// </summary>
        public bool invoiceFlag { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string invoiceTitle { get; set; }
        /// <summary>
        /// 发票内容
        /// </summary>
        public string invoiceNote { get; set; }
        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal invoiceAmount { get; set; }

        /// <summary>
        /// 是否代收货款
        /// </summary>
        public string isPaymentCollected { get; set; }

        /// <summary>
        /// 代收货款金额
        /// </summary>
        public decimal collectingPaymentAmount { get; set; }

        /// <summary>
        /// 快递商编码
        /// </summary>
        public string logisticsProviderCode { get; set; }

        /// <summary>
        /// 邮费
        /// </summary>
        public string shippingAmount { get; set; }

        private string _extTradeId;
        /// <summary>
        /// 网店零售订单传ERP单号
        /// </summary>
        public string extTradeId
        {
            get
            {
                if (_extTradeId==null)
                {
                    return null;
                }
                var tradeIds = _extTradeId.Split('/');
                //合并重复单号
                if (tradeIds.Length <= 1)
                {
                    return _extTradeId;
                }
                return string.Join("/", tradeIds.Union(tradeIds));
            }
            set
            {
                _extTradeId = value;
            }
        }
        /// <summary>
        /// 网店零售换订单传ERP单号
        /// </summary>
        public string refOrderCode { get; set; }

        public string orderSource { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string sellerName { get; set; }
        /// <summary>
        /// 道具级别
        /// </summary>
        public string udf6 { get; set; }
        /// <summary>
        /// 卖家备注
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 开启单头
        /// </summary>
        public bool udfFlag { get; set; }
        public string udf5 { get; set; }
      
        public List<item> items { get; set; }
        public recipient recipient { get; set; }

        public SyncSalesOrderInfo()
        {
            items = new List<item>();
            recipient = new recipient();
        }

    }

}
