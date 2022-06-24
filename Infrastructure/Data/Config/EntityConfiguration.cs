using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(120);
            builder.Property(e => e.Company).HasMaxLength(120);
            builder.Property(e => e.Address).HasMaxLength(150);
            builder.Property(e => e.City).HasMaxLength(120);
            builder.Property(e => e.State).HasMaxLength(35);
            builder.Property(e => e.Zip).HasMaxLength(18);
            builder.Property(e => e.Phone).HasMaxLength(35);
            builder.Property(e => e.Email).HasMaxLength(80);
            builder.Property(e => e.Type).HasMaxLength(35);
        }
    }
}