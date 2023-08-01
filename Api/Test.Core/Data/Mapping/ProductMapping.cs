
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Core.Entities;

namespace Test.Data.Mapping
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasOne(p => p.Store)
                .WithMany(o => o.Products)
                .HasForeignKey(orderItem => orderItem.StoreId);

            base.Configure(builder);
        }
    }
}
