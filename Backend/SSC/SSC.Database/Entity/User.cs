using SSC.Core.Base.Entity.Abstraction;
using SSC.DataLayer.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSC.Database.Entity
{
    public class User : BaseEntity
    {
        [StringLength(50)]
        public string UserCode { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
