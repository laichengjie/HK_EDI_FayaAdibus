using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.ShopModel;

namespace HK_EDI.Service.ShopModel
{
    public partial class ShopDataService : IShopDataService
    {
        private readonly IRepository<ShopData> _shopRepository;

        public ShopDataService(IRepository<ShopData> shopRepository)
        {
            this._shopRepository = shopRepository;
        }

        public IList<ShopData> GetList(string shopType)
        {
            var query = _shopRepository.Table;
            if (!string.IsNullOrEmpty(shopType))
            {
                int shopTypeId = Convert.ToInt32(shopType);
                query = query.Where(u => u.ShopType == shopTypeId);
            }
            query = query.OrderBy(t =>t.ShopName);
            var tasks = query.ToList();
            return tasks;
        }
    }
}
