using Microsoft.EntityFrameworkCore;
using Test.Core;
using Test.Data.Mapping;
using System.Data;
using System.Reflection;
namespace Test.Core.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var typeConfiguration in typeConfigurations)
            {
                dynamic configuration = Activator.CreateInstance(typeConfiguration);
                modelBuilder.ApplyConfiguration(configuration);
            }

            base.OnModelCreating(modelBuilder);
        }
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        private void PreModifyBeforeSave()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.LastTime = DateTime.Now;
                        entry.Entity.CreatedById = 1;
                        entry.Entity.LastUserId = 1;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastTime = DateTime.Now;
                        entry.Entity.LastUserId = 1;
                        entry.OriginalValues["LastModified"] = entry.Entity.LastModified;
                        break;
                }
            }
        }

        public override int SaveChanges()
        {
            PreModifyBeforeSave();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            PreModifyBeforeSave();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


    }
}
