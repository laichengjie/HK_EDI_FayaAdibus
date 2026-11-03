using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
    public class PaymentMethod : BaseEntity
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        //public string ShopID { get; set; }
        /// <summary>
        /// 店铺收款方式ID
        /// </summary>
        public string ReceiptTypeID { get; set; }
        /// <summary>
        /// 店铺收款方式代码
        /// </summary>
        public string ReceiptTypeCode { get; set; }
        /// <summary>
        /// Adibus支付方式
        /// </summary>
        public string AdibusSystemDef { get; set; }
    }
}
