using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Models.DiscountSet
{
    public class DiscountSetModel : BaseNopModel
    {
        public string ID { get; set; }
        public string Xuhao { get; set; }
        /// <summary>
        /// 折扣类型
        /// </summary>
        public string DiscountType { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Checker { get; set; }
        public string CheckerName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string ModifyDTM { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}