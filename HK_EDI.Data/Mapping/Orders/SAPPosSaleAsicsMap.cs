using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Orders
{
    public partial class AdibusPosSaleAsicsMap : NopEntityTypeConfiguration<AdibusPosSaleAsics>
    {
        public AdibusPosSaleAsicsMap(
            )
        {
            this.ToTable("HK_Adibus_PosSale_Asics");
            this.HasKey(m => m.Id);
        }
    }
}
