using SSC.Core.Base.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Entity.Abstraction
{
    /// <summary>
    /// Entity cho entity không xoá hoàn toàn, có kiểu dữ liệu khoá chính là <see cref="int"/>
    /// </summary>
    public abstract class BaseDeleteableEntity : BaseDeleteableEntity<int>, IIdentity<int>, IDatetimeStampable, ISoftDelete
    {

    }

    /// <summary>
    /// Entity cho entity không xoá hoàn toàn, có kiểu dữ liệu khoá chính là <typeparamref name="T"/>
    /// </summary>
    public abstract class BaseDeleteableEntity<T> : BaseEntity<T>, IIdentity<T>, IDatetimeStampable, ISoftDelete where T : IComparable
    {
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
