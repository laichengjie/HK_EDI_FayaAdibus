using HK_EDI.Core.Domain.Logging;

namespace HK_EDI.Data.Mapping.Logging
{
    public partial class LogMap : NopEntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("HK_Adibus_Log");
            this.HasKey(l => l.Id);
            this.Property(l => l.ShortMessage).IsRequired();
            this.Property(l => l.IpAddress).HasMaxLength(200);

            this.Ignore(l => l.LogLevel);

            this.HasOptional(l => l.Customer)
                .WithMany()
                .HasForeignKey(l => l.CustomerId)
            .WillCascadeOnDelete(true);

        }
    }
}