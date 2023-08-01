using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Core;
namespace Test.Data.Mapping
{
    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(t => t.Id).HasColumnName("Id").ValueGeneratedOnAdd();
		}
	}
}
