using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.Setting
{
    public class URAdibusPurRejectApiSettingModel : BaseModel
    {
        public string CompanyId { get; set; }

        public string OSInterfaceID { get; set; }


        public string Sign { get; set; }
    }
}