using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class GigConfiguration : IEntityTypeConfiguration<Gig>
    {
        public void Configure(EntityTypeBuilder<Gig> builder)
        {
            builder.Property(g => g.Id).IsRequired();
            builder.Property(g => g.Date).HasColumnType("datetime2");
            builder.Property(g => g.Pay).HasColumnType("decimal(18,2)");
            builder.HasOne(g => g.Venue).WithMany().HasForeignKey(g => g.VenueId);
            builder.Property(g => g.Band).HasMaxLength(120);
        }
    }
}