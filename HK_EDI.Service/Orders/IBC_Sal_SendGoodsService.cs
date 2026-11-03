using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public interface IBC_Sal_SendGoodsService
    {
        void AddBC_Sal_SendGoods(BC_Sal_SendGoods m);
    }
}
