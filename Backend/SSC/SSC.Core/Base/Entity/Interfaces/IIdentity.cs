using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Entity.Interfaces
{
    /// <summary>
    /// Interface cho kiểu entity có khoá chính là <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu cho khoá chính</typeparam>
    public interface IIdentity<T> where T : IComparable
    {
        public T Id { get; set; }
    }

    /// <summary>
    /// Interface cho kiểu entity có khoá chính là <see cref="int"/>
    /// </summary>
    public interface IIdentity : IIdentity<int> { }

    /// <summary>
    /// Interface cho kiểu entity có khoá chính là <see cref="Guid"/>
    /// </summary>
    public interface IGuidIdentity : IIdentity<Guid> { }

}
