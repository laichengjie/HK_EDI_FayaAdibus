using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Orders
{
    public partial class JobOrderTraceDetailMap : NopEntityTypeConfiguration<JobOrderTraceDetail>
    {
        public JobOrderTraceDetailMap(
            )
        {
            this.ToTable("HK_Adibus_JobOrderTraceDetail");
            this.HasKey(m => m.Id);
        }
    }
}
