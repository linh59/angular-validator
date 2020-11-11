using SSC.Core.Base.Entity.Interfaces;
using System;

namespace SSC.Database.Entity
{
    public class UserRole : IDatetimeStampable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
