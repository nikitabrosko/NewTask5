using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Date)
                .IsRequired();

            builder.Property(o => o.Sum)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders);
            
            builder.HasOne(o => o.Manager)
                .WithMany(m => m.Orders);

            builder.HasOne(o => o.Product)
                .WithMany(p => p.Orders);
        }
    }
}