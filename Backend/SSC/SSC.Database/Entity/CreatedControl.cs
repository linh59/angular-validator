using System;
using System.Text;

namespace SSC.Database.Entity
{
    public class CreatedControl : BaseControl
    {
        public string Value { get; set; }
        public virtual CreatedForm BelongTo { get; set; }
        
    }
}
