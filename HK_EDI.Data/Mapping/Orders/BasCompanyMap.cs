using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Orders
{
    public partial class BasCompanyMap : NopEntityTypeConfiguration<BasCompany>
    {
        public BasCompanyMap(
            )
        {
            this.ToTable("Bas_Company");
            this.HasKey(m => m.CompanyID);
            this.Ignore(m => m.Id);
        }
    }
}
