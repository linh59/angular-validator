using System.Collections.Generic;

namespace SSC.Database.Entity
{
    public class CreatedTableControl : BaseControl
    {
        public virtual ICollection<TableValue> Values { get; set; }
        public virtual CreatedForm BelongToTable { get; set; }
    }
}
