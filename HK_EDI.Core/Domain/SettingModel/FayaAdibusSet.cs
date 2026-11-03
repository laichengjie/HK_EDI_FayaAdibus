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
    public class FayaAdibusSet : ISettings
    {
        public string CompanyId { get; set; }
        public string CheckerID { get; set; }
        public string VendCustID { get; set; }
    }
}
