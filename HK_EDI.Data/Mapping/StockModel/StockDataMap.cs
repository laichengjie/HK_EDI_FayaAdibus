using HK_EDI.Core.Domain.StockModel;
using HK_EDI.Data.Mapping;

namespace HK_EDI.Data.Mapping.StockModel
{
    public class StockDataMap 
    {
        public string ErpStockID { get; set; }
        public string ErpStockCode { get; set; }
        public string ErpStockName { get; set; }
    }
}
