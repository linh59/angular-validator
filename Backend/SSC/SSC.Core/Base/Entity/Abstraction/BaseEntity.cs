using SSC.Core.Arguments.Attributes;
using SSC.Core.Base.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSC.Core.Base.Entity.Abstraction
{

    /// <summary>
    /// Entity cơ bản để kế thừa, kiểu dữ liệu khoá chính là <see cref="int"/>
    /// </summary>
    public abstract class BaseEntity : BaseEntity<int>
    {
    }
    
    /// <summary>
    /// Entity cơ bản để kế thừa, kiểu dữ liệu khoá chính là <see cref="Guid"/>
    /// </summary>
    public abstract class BaseGuidEntity : BaseEntity<Guid>, IGuidIdentity
    {
    }

    /// <summary>
    /// Entity cơ bản để kế thừa, kiểu dữ liệu khoá chính là <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Kiểu khoá chính</typeparam>
    public abstract class BaseEntity<T> : IIdentity<T>, IDatetimeStampable where T : IComparable
    {
        [Key]
        public T Id { get; set; }
        [IgnoreUpdate]
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
