using Microsoft.EntityFrameworkCore;
using SSC.Core.Arguments.Exceptions;
using SSC.Core.Base.Entity.Interfaces;
using SSC.Core.Base.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Core.Base.Infrastructure.Abstraction
{
    /// <summary>
    /// Repository cho bất đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    /// <typeparam name="TKey">Kiểu khoá chính</typeparam>
    public class GenericAsyncRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey>, IDisposable where TEntity : class, IIdentity<TKey> where TKey : IComparable
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> dbset;
        private bool disposed = false;

        public GenericAsyncRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            dbset = dbContext.Set<TEntity>();
        }

        #region Select Function
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetRootResultAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                return await orderBy(query).IgnoreQueryFilters().ToListAsync();
            }
            return await query.IgnoreQueryFilters().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            var item = await dbset.FindAsync(id);
            if (item == null)
            {
                throw new NotFoundException();
            }
            return item;
        }
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null) => dbset.FirstOrDefaultAsync(predicate);
        #endregion

        #region Insert Function
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbset.AddAsync(entity);
            return entity;
        }
        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbset.AddRangeAsync(entities);
            return entities;
        }
        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(params TEntity[] entities) => await AddRangeAsync(entities);
        #endregion

        #region Update Function
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                dbset.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            });
            return entity;
        }

        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            dbset.UpdateRange(entities);
            return Task.FromResult(entities);
        }
        #endregion

        #region Delete Function
        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await dbset.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Cannot find {nameof(TEntity)} has Id {id}");
            }
            await DeleteAsync(entity);
        }
        public virtual Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() =>
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    dbset.Attach(entity);
                }
                dbset.Remove(entity);
            });
        }
        public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() =>
            {
                dbset.RemoveRange(entities);
            });
        }
        public virtual async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> conditions)
        {
            var deleteEntities = dbset.Where(conditions);
            if (deleteEntities.Any())
            {
                await DeleteRangeAsync(deleteEntities);
            }
        }
        #endregion

        public virtual async Task SaveAsync() => await _dbContext.SaveChangesAsync();
        public Task BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

        public Task CommitTransactionAsync() => _dbContext.Database.CurrentTransaction.CommitAsync();

        public Task RollbackTransactionAsync() => _dbContext.Database.CurrentTransaction.RollbackAsync();

        public ValueTask DisposeTransactionAsync() => _dbContext.Database.CurrentTransaction.DisposeAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Repository cho bất đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    public class GenericAsyncRepository<TEntity> : GenericAsyncRepository<TEntity, int>, IAsyncRepository<TEntity>, IDisposable where TEntity : class, IIdentity<int>
    {
        public GenericAsyncRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
