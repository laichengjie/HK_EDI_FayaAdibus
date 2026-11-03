using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Models.VendCustSet
{
    public class ErpVendCustModel : BaseNopModel
    {
        public string ID { get; set; }
        public string Xuhao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErpVendCustID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErpVendCustCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErpVendCustName { get; set; }
        public string OperationType { get; set; }
        public string OperationTypeName { get; set; }
        public string BillState { get; set; }
        public string BillStateName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string ModifyDTM { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
        public IList<SelectListItem> VendCustLists { get; set; }
    }

    public class CorVendCustModel
    {

        public string ID { get; set; }
        public string Xuhao { get; set; }
        /// <summary>
        /// Erp仓库
        /// </summary>
        public string ErpVendCustID { get; set; }
        /// <summary>
        /// 仓库代码
        /// </summary>
        public string CorVendCustCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string CorVendCustName { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Checker { get; set; }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string CheckerName { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string ModifyDTM { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}