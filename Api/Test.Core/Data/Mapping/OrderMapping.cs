
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Core.Entities;

namespace Test.Data.Mapping
{
    public class OrderMapping : EntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasOne(order => order.Customer)
              .WithMany(o => o.Orders)
              .HasForeignKey(orderItem => orderItem.CustomerId);

            base.Configure(builder);
        }
    }
}
