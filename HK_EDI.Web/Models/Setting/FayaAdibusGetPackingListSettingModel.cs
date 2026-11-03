using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class FayaAdibusGetPackingListSettingModel : BaseModel
    {
        public string CompanyId { get; set; }
        public string CheckerID { get; set; }
        public string SERVICECODE { get; set; }
        /// <summary>
        /// 获取地址
        /// </summary>
        public string GetPackingListUrl { get; set; }
        public int GetDate { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string SetcustomerCode { get; set; }
    }
}