using SSC.Core.Base.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Core.Base.Infrastructure.Interface
{
    /// <summary>
    /// Interface cho repository bất đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    /// <typeparam name="TKey">Kiểu khoá chính</typeparam>
    public interface IAsyncRepository<TEntity, TKey> : IDisposable, IAsyncTransactionRepository where TKey : IComparable where TEntity : IIdentity<TKey>
    {
        #region Select Function
        /// <summary>
        /// Truy vấn tất cả
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Get default with filter
        /// </summary>
        /// <param name="filter">Biểu thức filter</param>
        /// <param name="orderBy">Biểu thức sắp xếp</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// Truy vấn với filter, nhưng bỏ qua global filter
        /// </summary>
        /// <param name="filter">Biểu thức filter</param>
        /// <param name="orderBy">Biểu thức sắp xếp</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<TEntity>> GetRootResultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// Truy vấn cụ thể với khoá chính
        /// </summary>
        /// <param name="id">Khoá chính</param>
        /// <returns><typeparamref name="TEntity"/>Thực thể</returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Truy vấn cụ thể với điều kiện
        /// </summary>
        /// <param name="expression">Biểu thức điều kiện</param>
        /// <returns><typeparamref name="TEntity"/>Thực thể</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region Insert Function
        /// <summary>
        /// Thêm thực thể vào database
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Thực thể đã thêm</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Thêm thực thể vào database
        /// </summary>
        /// <param name="entities">Collection của các thực thể cần thêm</param>
        /// <returns>Collection của các thực thể đã thêm</returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Thêm thực thể vào database
        /// </summary>
        /// <param name="entities">Collection của các thực thể cần thêm</param>
        /// <returns>Collection của các thực thể đã thêm</returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(params TEntity[] entities);
        #endregion

        #region Update function
        /// <summary>
        /// Cập nhật dữ liệu của <paramref name="entity"/> vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Entity cần cập nhật</param>
        /// <returns>Entity đã được cập nhật</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entity);
        #endregion

        #region Delete Function
        /// <summary>
        /// Xoá entity với id chỉ định
        /// </summary>
        /// <param name="id">Id của entity cần xoá</param>
        Task DeleteAsync(TKey id);
        /// <summary>
        /// Xoá entity chỉ định
        /// </summary>
        /// <param name="entity">Entity cần xoá</param>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// Xoá các entity chỉ định
        /// </summary>
        /// <param name="entities">Collection của entity cần xoá</param>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Xoá các entity chỉ định
        /// </summary>
        /// <param name="conditions">Điều kiện của entity cần xoá</param>
        Task DeleteRangeAsync(Expression<Func<TEntity, bool>> conditions);
        #endregion

        /// <summary>
        /// Lưu các thay đổi vào cơ sở dữ liệu
        /// </summary>
        Task SaveAsync();
    }

    /// <summary>
    /// Interface cho repository bất đồng bộ
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    public interface IAsyncRepository<TEntity> : IAsyncRepository<TEntity, int>, IDisposable, IAsyncTransactionRepository where TEntity : IIdentity<int>
    {

    }
}
