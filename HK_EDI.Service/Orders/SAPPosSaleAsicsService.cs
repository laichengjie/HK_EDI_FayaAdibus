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
    public class AdibusPosSaleAsicsService : IAdibusPosSaleAsicsService
    {
        public readonly IRepository<AdibusPosSaleAsics> _AdibusPosSaleAsicsService;
        private readonly IDbContext _dbContext;
        public AdibusPosSaleAsicsService(
            IRepository<AdibusPosSaleAsics> AdibusPosSaleAsicsService,
            IDbContext dbContext
            )
        {
            this._AdibusPosSaleAsicsService = AdibusPosSaleAsicsService;
            this._dbContext = dbContext;
        }

        public void AddAdibusPosSaleAsics(AdibusPosSaleAsics m)
        {
            _AdibusPosSaleAsicsService.Insert(m);
        }

    }
}
