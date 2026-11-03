using HK_EDI.Core.Domain.ShopModel;
using HK_EDI.Core.Domain.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.StockModel
{
    public partial interface IStockDataService
    {
        IList<StockData> GetList(string stockType);
    }
}
