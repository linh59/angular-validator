using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Entity.Interfaces
{
    /// <summary>
    /// Interface cho việc xoá không hoàn toàn
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// Cho biết xoá hay chưa
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Thời gian thực hiện việc xoá
        /// </summary>
        public DateTime DeletedAt { get; set; }
    }
}
