using HK_EDI.Core.Domain.StockModel;
using HK_EDI.Core.Domain.VendCustModel;
using HK_EDI.Data.Mapping;

namespace HK_EDI.Data.Mapping.VendCustModel
{
    public class VendCustDataModel : NopEntityTypeConfiguration<VendCustData>
    {
        public VendCustDataModel()
        {
            this.ToTable("vwSD_VendorManerage");
            base.HasKey(m => m.VendCustID);
            this.Ignore(m => m.Id);
        }
    }
}
