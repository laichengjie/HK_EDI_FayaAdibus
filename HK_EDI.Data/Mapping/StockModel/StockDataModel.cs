using HK_EDI.Core.Domain.StockModel;
using HK_EDI.Data.Mapping;

namespace HK_EDI.Data.Mapping.StockModel
{
    public class StockDataModel : NopEntityTypeConfiguration<StockData>
    {
        public StockDataModel()
        {
            this.ToTable("Bas_Stock");
            base.HasKey(m => m.StockID);
            this.Ignore(m => m.Id);
        }
    }
}
