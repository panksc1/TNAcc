using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BandConfiguration : IEntityTypeConfiguration<Band>
    {
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(120);
        }
    }
}