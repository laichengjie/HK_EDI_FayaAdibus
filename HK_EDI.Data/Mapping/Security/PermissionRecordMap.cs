using HK_EDI.Data.Mapping;
using HK_EDI.Core.Domain.Security;

namespace HK_EDI.Data.Mapping.Security
{
    public partial class PermissionRecordMap : NopEntityTypeConfiguration<PermissionRecord>
    {
        public PermissionRecordMap()
        {
            this.ToTable("HK_Adibus_PermissionRecord");
            this.HasKey(pr => pr.Id);
            this.Property(pr => pr.Name).IsRequired();
            this.Property(pr => pr.SystemName).IsRequired().HasMaxLength(255);
            this.Property(pr => pr.Category).IsRequired().HasMaxLength(255);

            this.HasMany(pr => pr.CustomerRoles)
                .WithMany(cr => cr.PermissionRecords)
                .Map(m => m.ToTable("HK_Adibus_PermissionRecord_Role_Mapping"));
        }
    }
}