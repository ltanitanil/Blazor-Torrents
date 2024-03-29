﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blazor.Server.DataAccessLayer.Context.Torrents;
using Blazor.Server.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.DataAccessLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly TorrentsContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(TorrentsContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            var result = _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<long> MaxAsync(Expression<Func<TEntity, long>> expression) =>
                    await _dbSet.AsNoTracking().MaxAsync(expression);

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression,
            List<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            var query = _dbSet.AsNoTracking();

            if (includeProperties != null)
                query = IncludeProperties(query, includeProperties);

            return await query.SingleOrDefaultAsync(expression);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
                    List<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            var query = _dbSet.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);
            if (includeProperties != null)
                query = IncludeProperties(query, includeProperties);

            return query;
        }

        private static IQueryable<TEntity> IncludeProperties(IQueryable<TEntity> query,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties) =>
            includeProperties.Aggregate(query, (current, include) => current.Include(include));
    }
}
