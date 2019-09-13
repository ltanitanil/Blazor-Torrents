using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Entities;

namespace Blazor.Server.DataAccessLayer.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            List<Expression<Func<TEntity, object>>> includes = null);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression,
            List<Expression<Func<TEntity, object>>> includeProperties = null);
        Task<long> MaxAsync(Expression<Func<TEntity, long>> expression);
    }
}
