using HK_EDI.Data.Mapping;
using HK_EDI.Core.Domain.ShopModel;

namespace HK_EDI.Data.Mapping.ShopModel
{
    public class ShopDataMap : NopEntityTypeConfiguration<ShopData>
    {
        public ShopDataMap()
        {
            this.ToTable("Bas_Shop");
            base.HasKey(m => m.ShopID);
            this.Ignore(m => m.Id);
        }
    }
}
