using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Models.OrderTrace
{
    public class OrderTraceListModel : BaseNopModel
    {
        public OrderTraceListModel()
        {
            OrderStatus = new List<SelectListItem>();
            OrderTypes = new List<SelectListItem>();
            WMSSystems = new List<SelectListItem>();
            ShopLists= new List<SelectListItem>();
        }

        [Display(Name = "公司ID")]
        public string SearchCompanyId { get; set; }

        [Display(Name = "订单号")]
        public string SearchSignCode { get; set; }

        [Display(Name = "单据号")]
        public string SearchNoticeBillNo { get; set; }

        [Display(Name = "采购单号")]
        public string SearchCgBillNo { get; set; }

        [Display(Name = "单据类型")]
        public string SearchBillTypeID { get; set; }

        [Display(Name = "状态")]
        public Int32? SearchOpResultStatus { get; set; }

        [Display(Name = "创建开始时间")]
        public DateTime? CreateDateFrom { get; set; }

        [Display(Name = "创建结束时间")]
        public DateTime? CreateDateTo { get; set; }

        [Display(Name = "修改开始时间")]
        public DateTime? ReadDateFrom { get; set; }

        [Display(Name = "修改结束时间")]
        public DateTime? ReadDateTo { get; set; }

        [Display(Name = "自动撤销开始时间")]
        public DateTime? CancelDateFrom { get; set; }

        [Display(Name = "自动撤销结束时间")]
        public DateTime? CancelDateTo { get; set; }

        [Display(Name = "订单类型")]
        public int? SearchOrderType { get; set; }

        [Display(Name = "Adibus接口类型")]
        public string SearchOSInterfaceID { get; set; }

        [Display(Name = "零售接口(店铺)")]
        public string ShopID { get; set; }

        [Display(Name = "零售接口(销售日期)")]
        public DateTime? BillDate { get; set; }

        public IList<SelectListItem> WMSSystems { get; set; }

        public IList<SelectListItem> OrderStatus { get; set; }

        public IList<SelectListItem> OrderTypes { get; set; }

        public IList<SelectListItem> ShopLists { get; set; }

    }
}