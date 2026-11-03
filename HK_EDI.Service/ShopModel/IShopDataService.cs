using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core.Domain.ShopModel;

namespace HK_EDI.Service.ShopModel
{
    public partial interface IShopDataService
    {
        IList<ShopData> GetList(string shopType);
    }
}
