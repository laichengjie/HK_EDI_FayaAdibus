using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class BestSettingModel
    {
        public string Url { get; set; }
        /// <summary>
        /// 卖家编码
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// 仓库代码
        /// </summary>
        public string WarehouseCode { get; set; }
        /// <summary>
        /// 授权用户
        /// </summary>
        public string PartnerID { get; set; }
        /// <summary>
        /// 授权码
        /// </summary>
        public string PartnerKey { get; set; }
        /// <summary>
        /// ERP用户ID
        /// </summary>
        public string ERPUserID { get; set; }

        public string O2O_PartnerID { get; set; }
        public string O2O_PartnerKey { get; set; }
        public string O2O_Url { get; set; }
        /// <summary>
        /// 调拨方式
        /// </summary>
        public string MoveModeID { get; set; }
        /// <summary>
        /// 调账仓库代码
        /// </summary>
        public string Adjustmen { get; set; }
        /// <summary>
        /// 是否走账
        /// </summary>
        public string Moveaccount { get; set; }
    }
}