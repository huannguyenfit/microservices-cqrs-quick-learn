using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Test.Core.Data
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        DbContext Context { get; }

        IQueryable<TEntity> Table { get; }

        TEntity GetById(long id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void DeleteById(long id);

        Task<TEntity> GetByIdAsync(long id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteByIdAsync(long id);
    }
}
