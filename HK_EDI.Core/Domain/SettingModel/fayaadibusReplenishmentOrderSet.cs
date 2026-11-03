using HK_EDI.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.SettingModel
{
    /// <summary>
    /// 
    /// </summary>
    public class fayaadibusReplenishmentOrderSet : ISettings
    {
        public string CompanyId { get; set; }
        public string CheckerID { get; set; }
        public string SERVICECODE { get; set; }
        /// <summary>
        /// 补货单获取地址
        /// </summary>
        public string GetReplenishmentOrderUrl { get; set; }
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
