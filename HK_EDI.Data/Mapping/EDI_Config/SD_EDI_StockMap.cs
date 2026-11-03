using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.EDI_Config
{
    public class SD_EDI_StockMap : NopEntityTypeConfiguration<HK_EDI.Core.Domain.EDI_Config.SD_Adibus_Interface>
    {
        public SD_EDI_StockMap()
        {
            this.ToTable("SD_Adibus_Interface");
            base.HasKey(m => new { m.CompanyID, m.OSInterfaceID });
            this.Ignore(m => m.Id);
            this.Ignore(m => m.SD_Adibus_InterfaceDetail);
        }
    }
}
