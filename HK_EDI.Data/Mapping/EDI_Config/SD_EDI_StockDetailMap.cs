using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.SD_Adibus_Interface
{
    public class SD_EDI_StockDetailMap : NopEntityTypeConfiguration<HK_EDI.Core.Domain.EDI_Config.SD_Adibus_InterfaceDetail>
    {
        public SD_EDI_StockDetailMap()
        {
            this.ToTable("SD_Adibus_InterfaceDetail");
            base.HasKey(m => new { m.CompanyID, m.OSInterfaceID, m.StockID });
            this.Ignore(m => m.Id );
        }
    }
}
