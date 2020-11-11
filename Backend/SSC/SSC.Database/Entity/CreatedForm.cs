using SSC.Core.Base.Entity.Abstraction;
using SSC.Core.Base.Entity.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSC.Database.Entity
{
    public class CreatedForm : BaseEntity, IOwnedRecord<string>
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Creator { get; set; }
        [StringLength(50)]
        public string RequestFor { get; set; }

        public virtual ICollection<CreatedControl> CreatedControls { get; set; } = new List<CreatedControl>();
        public virtual ICollection<CreatedTableControl> CreatedTableControls { get; set; } = new List<CreatedTableControl>();
        public virtual ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}
