using Microsoft.EntityFrameworkCore;
using SSC.Core.Arguments.Exceptions;
using SSC.Core.Base.Entity.Interfaces;
using SSC.Core.Base.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SSC.Core.Base.Infrastructure.Abstraction
{
    /// <summary>
    /// Repository cho đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    /// <typeparam name="TKey">Kiểu khoá chính</typeparam>
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey>, IDisposable where TEntity : class, IIdentity<TKey> where TKey : IComparable
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> dbset;
        private bool disposed = false;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            dbset = dbContext.Set<TEntity>();
        }

        #region Select Function
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }
        public virtual IEnumerable<TEntity> GetRootResult(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                return orderBy(query).IgnoreQueryFilters().ToList();
            }
            return query.IgnoreQueryFilters().ToList();
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbset.ToList();
        }

        public virtual TEntity GetById(TKey id) => dbset.Find(id);
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate = null) => dbset.FirstOrDefault(predicate);
        #endregion

        #region Insert Function
        public virtual TEntity Add(TEntity entity)
        {
            dbset.Add(entity);
            return entity;
        } 
        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            dbset.AddRange(entities);
            return entities;
        }
        public virtual IEnumerable<TEntity> AddRange(params TEntity[] entities) => AddRange(entities);
        #endregion

        #region Update Function
        public virtual TEntity Update(TEntity entity)
        {
            dbset.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        #endregion

        #region Delete Function
        public virtual void Delete(TKey id)
        {
            var entity = dbset.Find(id);
            if (entity == null)
            {
                throw new NotFoundException($"Không thể tìm {nameof(TEntity)} có Id {id}");
            }
            Delete(entity);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbset.Attach(entityToDelete);
            }
            dbset.Remove(entityToDelete);
        }
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbset.RemoveRange(entities);
        }
        public virtual void DeleteRange(Expression<Func<TEntity, bool>> conditions)
        {
            var deleteEntities = dbset.Where(conditions);
            if (deleteEntities.Any())
            {
                DeleteRange(deleteEntities);
            }
        }
        #endregion

        public virtual void Save() => _dbContext.SaveChanges();

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
    /// Repository cho đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    public class GenericRepository<TEntity> : GenericRepository<TEntity, int>, IRepository<TEntity>, IDisposable where TEntity : class, IIdentity<int>
    {
        public GenericRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
