
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Core.Entities;

namespace Test.Data.Mapping
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
          
            base.Configure(builder);
        }
    }
}
