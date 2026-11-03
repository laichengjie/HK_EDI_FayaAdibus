using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Models.GoodPostRecord
{
    public class GoodPostRecordModel : BaseNopModel
    {
        public GoodPostRecordModel()
        {
            WMSSystems = new List<SelectListItem>();
        }
        [Display(Name = "物流系统")]
        public string SearchOSStockID { get; set; }
        [Display(Name="产品条码")]
        public string SkuCode { get; set; }

        [Display(Name = "开始时间")]
        public DateTime? CreateDateFrom { get; set; }

        [Display(Name = "结束时间")]
        public DateTime? CreateDateTo { get; set; }

        [Display(Name="是否成功")]
        public bool IsSuccess { get; set; }

        public IList<SelectListItem> WMSSystems { get; set; }
    }
}