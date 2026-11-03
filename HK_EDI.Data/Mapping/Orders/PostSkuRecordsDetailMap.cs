using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Orders
{
    public class PostSkuRecordsDetailMap : NopEntityTypeConfiguration<PostSkuRecordsDetail>
    {
        public PostSkuRecordsDetailMap(
            )
        {
            this.ToTable("HK_Adibus_PostSkuRecordsDetail");
            this.HasKey(m => m.Id);
        }
    }
}
