using SSC.Core.Arguments.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Entity.Interfaces
{
    /// <summary>
    /// Interface for datetime stamp
    /// </summary>
    public interface IDatetimeStampable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
