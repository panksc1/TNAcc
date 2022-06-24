using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(120);
            builder.Property(e => e.At).HasMaxLength(120);
            builder.Property(e => e.Address).HasMaxLength(150);
            builder.Property(e => e.City).HasMaxLength(120);
            builder.Property(e => e.State).HasMaxLength(35);
            builder.Property(e => e.Zip).HasMaxLength(18);
        }
    }
}