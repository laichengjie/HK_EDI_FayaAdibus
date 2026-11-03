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
    public class NOMEPosSaleSettings : ISettings
    {
        public string CompanyId { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string Url { get; set; }

        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string StockCode { get; set; }
        public DateTime BeginDate { get; set; }



    }
}
