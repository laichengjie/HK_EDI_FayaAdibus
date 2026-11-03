using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.EDI_Config
{
    public class SD_Adibus_InterfaceDetail : BaseEntity
    {
        public string CompanyID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string OSInterfaceID { get; set; }

       
        public string StockID { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public string BelongsCompanyID { get; set; }
    }
}
