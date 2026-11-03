using HK_EDI.Core.Caching;
using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public class BasCompanyService : IBasCompanyService
    {
        private readonly ICacheManager _cacheManager;
        private IRepository<BasCompany> _iRepositoryBasCompany;
        private const string companykey = "hk_edi_company_service_cache";
        public BasCompanyService(
            ICacheManager cacheManager,
            IRepository<BasCompany> iRepositoryBasCompany
            )
        {
            this._cacheManager = cacheManager;
            this._iRepositoryBasCompany = iRepositoryBasCompany;
        }

        public BasCompany GetBasCompany(string CompanyId)
        {
            return GetBasCompanyALL().FirstOrDefault(t => t.CompanyID == CompanyId);
        }

        public List<BasCompany> GetBasCompanyALL()
        {
            return _cacheManager.Get(companykey, 60 * 24, () => { return _iRepositoryBasCompany.Table.ToList(); });
        }
    }
}
