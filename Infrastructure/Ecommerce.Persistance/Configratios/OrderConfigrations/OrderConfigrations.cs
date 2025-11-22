using Ecommerce.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistance.Configratios.OrderConfigrations
{
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(O => O.Subtotal).HasColumnType("decimal(8,2)");
            builder.HasOne(o => o.DeliveryMethod).WithMany().HasForeignKey(o => o.DeliveryMethodId);
            builder.OwnsOne(o => o.Address);

        }
    }
}
