using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public interface IBasCompanyService
    {
        BasCompany GetBasCompany(string CompanyId);

        List<BasCompany> GetBasCompanyALL();
    }
}
