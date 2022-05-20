using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class PayableConfiguration : IEntityTypeConfiguration<Payable>
    {
        public void Configure(EntityTypeBuilder<Payable> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.DatePaid).IsRequired();
            builder.Property(p => p.DatePaid).HasColumnType("datetime2");
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Entity).WithMany().HasForeignKey(p => p.EntityId);
            builder.HasOne(p => p.Gig).WithMany().HasForeignKey(p => p.GigId);
        }
    }
}