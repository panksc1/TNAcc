using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ReceivableConfiguration : IEntityTypeConfiguration<Receivable>
    {
        public void Configure(EntityTypeBuilder<Receivable> builder)
        {
            builder.Property(r => r.Id).IsRequired();
            builder.Property(r => r.DateReceived).HasColumnType("datetime2");
            builder.Property(r => r.AmountDue).HasColumnType("decimal(18,2)");
            builder.Property(p => p.AmountPaid).HasColumnType("decimal(18,2)");
            builder.HasOne(r => r.Entity).WithMany().HasForeignKey(r => r.EntityId);
            builder.HasOne(r => r.Gig).WithMany().HasForeignKey(r => r.GigId);
        }
    }
}