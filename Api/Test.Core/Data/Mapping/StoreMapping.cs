
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Core.Entities;

namespace Test.Data.Mapping
{
    public class StoreMapping : EntityTypeConfiguration<Store>
    {
        public override void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store");
            base.Configure(builder);
        }
    }
}
