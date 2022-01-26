using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => new { c.FirstName, c.LastName })
                .IsUnique();
            
            builder.Property(c => c.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.FullName)
                .HasComputedColumnSql("[LastName] + ' ' + [FirstName]");

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer);
        }
    }
}