using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class FayaAdibusSettingModel : BaseModel
    {
        public string CompanyId { get; set; }
        public string CheckerID { get; set; }
        public string VendCustID { get; set; }
    }
}