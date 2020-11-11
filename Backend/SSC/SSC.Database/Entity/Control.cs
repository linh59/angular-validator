using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Database.Entity
{
    public class Control: BaseControl
    {
        public virtual FormTemplate BelongTo { get; set; }
        public virtual FormTemplate BelongToTable { get; set; }
    }
    //public class Validation : BaseGuidEntity
    //{
    //    public ICollection<ValidationConstrait>
    //}

    //public class ValidationConstrait : BaseGuidEntity
    //{
    //    public string ValueType { get; set; }
    //    public object RootValue { get; set; }

    //    public bool Validate(object value)
    //    {
    //        var item = Convert.ChangeType(value, Type.GetType(ValueType));
    //        return true;
    //    }
    //}
}
