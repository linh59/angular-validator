using SSC.Core.Base.Entity.Abstraction;
using SSC.DataLayer.Enumerations;
using System.Collections.Generic;

namespace SSC.Database.Entity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public Permissions  Permissions { get; set; }
        public virtual ICollection<UserRole> Users { get; set; }
    }
}
