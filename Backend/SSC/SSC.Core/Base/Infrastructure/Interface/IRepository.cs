using SSC.Core.Base.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SSC.Core.Base.Infrastructure.Interface
{
    /// <summary>
    /// Interface cho repository đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    /// <typeparam name="TKey">Kiểu khoá chính</typeparam>
    public interface IRepository<TEntity, TKey> : IDisposable where TKey : IComparable where TEntity : IIdentity<TKey>
    {

        #region Select Function
        /// <summary>
        /// Truy vấn tất cả
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get default with filter
        /// </summary>
        /// <param name="filter">Biểu thức filter</param>
        /// <param name="orderBy">Biểu thức sắp xếp</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// Truy vấn với filter, nhưng bỏ qua global filter
        /// </summary>
        /// <param name="filter">Biểu thức filter</param>
        /// <param name="orderBy">Biểu thức sắp xếp</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TEntity> GetRootResult(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// Truy vấn cụ thể với khoá chính
        /// </summary>
        /// <param name="id">Khoá chính</param>
        /// <returns><typeparamref name="TEntity"/>Thực thể</returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Truy vấn cụ thể với điều kiện
        /// </summary>
        /// <param name="expression">Biểu thức điều kiện</param>
        /// <returns><typeparamref name="TEntity"/>Thực thể</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region Insert Function
        /// <summary>
        /// Thêm thực thể vào database
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Thực thể đã thêm</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Thêm thực thể vào database
        /// </summary>
        /// <param name="entities">Collection của các thực thể cần thêm</param>
        /// <returns>Collection của các thực thể đã thêm</returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Thêm thực thể vào database
        /// </summary>
        /// <param name="entities">Collection của các thực thể cần thêm</param>
        /// <returns>Collection của các thực thể đã thêm</returns>
        IEnumerable<TEntity> AddRange(params TEntity[] entities);
        #endregion

        #region Update Function
        /// <summary>
        /// Cập nhật dữ liệu của <paramref name="entity"/> vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Entity cần cập nhật</param>
        /// <returns>Entity đã được cập nhật</returns>
        TEntity Update(TEntity entity);
        #endregion

        #region Delete Function
        /// <summary>
        /// Xoá entity với id chỉ định
        /// </summary>
        /// <param name="id">Id của entity cần xoá</param>
        void Delete(TKey id);
        /// <summary>
        /// Xoá entity chỉ định
        /// </summary>
        /// <param name="entity">Entity cần xoá</param>
        void Delete(TEntity entity);
        /// <summary>
        /// Xoá các entity chỉ định
        /// </summary>
        /// <param name="entities">Collection của entity cần xoá</param>
        void DeleteRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// Xoá các entity chỉ định
        /// </summary>
        /// <param name="conditions">Điều kiện của entity cần xoá</param>
        void DeleteRange(Expression<Func<TEntity, bool>> conditions);
        #endregion

        /// <summary>
        /// Lưu các thay đổi vào cơ sở dữ liệu
        /// </summary>
        void Save();
    }

    /// <summary>
    /// Interface cho repository đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    public interface IRepository<TEntity> : IDisposable, IRepository<TEntity, int> where TEntity : IIdentity<int>
    {
    }
}
