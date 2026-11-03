using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.StockModel;

namespace HK_EDI.Service.StockModel
{
    public partial class StockDataService : IStockDataService
    {
        private readonly IRepository<StockData> _stockRepository;

        public StockDataService(IRepository<StockData> stockRepository)
        {
            this._stockRepository = stockRepository;
        }

        public IList<StockData> GetList(string stockType)
        {
            var query = _stockRepository.Table;
            if (!string.IsNullOrEmpty(stockType))
            {
                int shopTypeId = Convert.ToInt32(stockType);
                query = query.Where(u => u.StockType == shopTypeId);
            }
            query = query.OrderBy(t =>t.StockName);
            var tasks = query.ToList();
            return tasks;
        }
    }
}
