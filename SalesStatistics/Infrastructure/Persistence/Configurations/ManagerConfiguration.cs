using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => new { m.LastName })
                .IsUnique();

            builder.Property(m => m.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasMany(m => m.Orders)
                .WithOne(o => o.Manager);

            builder.Navigation(c => c.Orders)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}