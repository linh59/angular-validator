using SSC.Core.Base.Entity.Abstraction;
using SSC.Core.Base.Entity.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSC.Database.Entity
{
    public class FormTemplate : BaseDeleteableEntity, IOwnedRecord<string>
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public string Creator { get; set; }


        public virtual Category Category { get; set; }
        public virtual ICollection<CreatedForm> CreatedForms { get; set; } = new List<CreatedForm>();
        public virtual ICollection<FormAttribute> FormAttributes { get; set; } = new List<FormAttribute>();
        public virtual ICollection<Control> Controls { get; set; } = new List<Control>();
        public virtual ICollection<Control> TableControls { get; set; } = new List<Control>();
    }
}
