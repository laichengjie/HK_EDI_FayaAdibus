using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Orders
{
    public partial class JobOrderTraceErrDetailMap : NopEntityTypeConfiguration<JobOrderTraceErrDetail>
    {
        public JobOrderTraceErrDetailMap(
            )
        {
            this.ToTable("vmHK_Adibus_JobOrderTraceErrDetail");
            this.HasKey(m => m.Id);
        }
    }
}
