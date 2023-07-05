using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
namespace Test.Core.Data
{
    public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly ShopDbContext _context;

        private DbSet<TEntity> _entities;

        public EfRepository(ShopDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> Table => Entities;
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        public virtual DbContext Context => _context;
        public IQueryable<TEntity> Entity => throw new NotImplementedException();

        public virtual TEntity GetById(long id)
        {
            var entity = Entities.Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.Entities.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Update(entity);
            _context.SaveChanges();

        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            _context.SaveChanges();

        }


        public virtual void DeleteById(long id)
        {
            Delete(GetById(id));
            _context.SaveChanges();

        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            var entity =  await Entities.FindAsync(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            return entity;
        }


        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Task.FromResult(Entities.Add(entity));

            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Task.FromResult(Entities.Update(entity));
            await _context.SaveChangesAsync();

        }


        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            await DeleteAsync(await GetByIdAsync(id));
            await _context.SaveChangesAsync();

        }

    }


}
