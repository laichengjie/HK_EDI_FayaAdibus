using HK_EDI.Core.Domain.Customers;

namespace HK_EDI.Data.Mapping.Customers
{
    public partial class CustomerRoleMap : NopEntityTypeConfiguration<CustomerRole>
    {
        public CustomerRoleMap()
        {
            this.ToTable("HK_Adibus_CustomerRole");
            this.HasKey(cr => cr.Id);
            this.Property(cr => cr.Name).IsRequired().HasMaxLength(255);
            this.Property(cr => cr.SystemName).HasMaxLength(255);
        }
    }
}