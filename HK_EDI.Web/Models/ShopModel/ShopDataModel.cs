using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Models.ShopModel
{
    public class ShopDataModel : BaseNopModel
    {
        public ShopDataModel()
        {
            ShopList = new List<SelectListItem>();
        }
        [Display(Name = "店铺ID")]
        public string ShopID { get; set; }

        [Display(Name = "补传日期")]
        public DateTime DateTime { get; set; }

        [Display(Name = "公司代码")]
        public string CompanyId { get; set; }

        public IList<SelectListItem> ShopList { get; set; }
    }
}