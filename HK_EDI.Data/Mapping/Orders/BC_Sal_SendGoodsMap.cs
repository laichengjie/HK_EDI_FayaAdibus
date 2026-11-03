using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Data.Mapping.Orders
{
    public class BC_Sal_SendGoodsMap : NopEntityTypeConfiguration<BC_Sal_SendGoods>
    {
        public BC_Sal_SendGoodsMap(
           )
        {
            this.ToTable("BC_Sal_SendGoods");
            this.HasKey(m => m.Id);
        }
    }
}
