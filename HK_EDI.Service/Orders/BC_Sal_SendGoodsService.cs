using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public class BC_Sal_SendGoodsService : IBC_Sal_SendGoodsService
    {
        public readonly IRepository<BC_Sal_SendGoods> _BC_Sal_SendGoodsRepository;
        private readonly IDbContext _dbContext;
        public BC_Sal_SendGoodsService(
            IRepository<BC_Sal_SendGoods> BC_Sal_SendGoodsRepository,
            IDbContext dbContext
            )
        {
            this._BC_Sal_SendGoodsRepository = BC_Sal_SendGoodsRepository;
            this._dbContext = dbContext;
        }

        public void AddBC_Sal_SendGoods(BC_Sal_SendGoods m)
        {
            _BC_Sal_SendGoodsRepository.Insert(m);
        }

    }
}
