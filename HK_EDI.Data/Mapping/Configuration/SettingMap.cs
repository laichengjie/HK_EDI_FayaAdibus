using HK_EDI.Core.Domain.Configuration;

namespace HK_EDI.Data.Mapping.Configuration
{
    public partial class SettingMap : NopEntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            this.ToTable("HK_Adibus_Setting");
            this.HasKey(s => s.Id);
            this.Property(s => s.Name).IsRequired().HasMaxLength(200);
            this.Property(s => s.Value).IsRequired().HasMaxLength(2000);
        }
    }
}