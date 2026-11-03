using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Orders
{
    public partial class JobOrderTraceMap : NopEntityTypeConfiguration<JobOrderTrace>
    {
        public JobOrderTraceMap(
            )
        {
            this.ToTable("HK_Adibus_JobOrderTrace");
            this.HasKey(m => m.Id);
        }
    }
}
