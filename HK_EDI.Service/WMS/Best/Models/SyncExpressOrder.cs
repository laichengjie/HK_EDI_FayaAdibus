using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.Best.Models
{
    public class SyncExpressOrder
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        public string projectCode { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        public string customerOrderCode { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string actionType { get; set; }

        public sender sender { get; set; }

        public recipient recipient { get; set; }

        public SyncExpressOrder()
        {
            sender = new sender();
            recipient = new recipient();
        }
    }
}
