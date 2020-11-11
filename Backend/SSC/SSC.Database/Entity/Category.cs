using SSC.Core.Base.Entity.Abstraction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSC.Database.Entity
{
    public class Category : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<FormTemplate> FormTemplates { get; set; } = new List<FormTemplate>();
    }
}
