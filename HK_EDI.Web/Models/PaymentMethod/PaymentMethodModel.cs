using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Models.PaymentMethod
{
    public class PaymentMethodModel : BaseNopModel
    {
        public PaymentMethodModel()
        {
            ShopLists = new List<SelectListItem>();
        }

        public string ID { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 店铺ID
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
        /// 店铺收款方式名称
        /// </summary>
        public string ReceiptTypeName { get; set; }
        /// <summary>
        /// Adibus支付方式
        /// </summary>
        public string AdibusSystemDef { get; set; }
        public int IsDiscount { get; set; }
        public string IsDiscountName { get; set; }
        //public int Page { get; set; }

        //public int PageSize { get; set; }
        public IList<SelectListItem> ShopLists { get; set; }
    }
}