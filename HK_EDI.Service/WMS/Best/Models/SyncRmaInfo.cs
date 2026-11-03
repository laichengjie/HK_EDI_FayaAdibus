using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class SyncRmaInfo : SyncBaseModel
    {
        public string rmaCode { get; set; }
        public string actionType { get; set; }
        public string orderType { get; set; }
        public bool udfFlag { get; set; }
        public string udf5 { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string sellerName { get; set; }
        /// <summary>
        /// 快递商编码
        /// </summary>
        public string logisticsProviderCode { get; set; }
        /// <summary>
        /// 是否代收货款
        /// </summary>
        public string isPaymentCollected { get; set; }
        /// <summary>
        /// 卖家备注
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 网店零售订单传ERP单号
        /// </summary>
        public string refOrderCode { get; set; }
        public List<item> items { get; set; }
        public sender sender { get; set; }
        public SyncRmaInfo()
        {
            items = new List<item>();
            sender = new sender();
        }
    }
}
