using SSC.Core.Base.Entity.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace SSC.Database.Entity
{
    public class BaseControl : BaseGuidEntity
    {
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(10)]
        public string Style { get; set; }
        [StringLength(120)]
        public string Hint { get; set; }
        [StringLength(120)]
        public string Placeholder { get; set; }
        public int? MaxLength { get; set; }
        [StringLength(20)]
        public string SuffixIcon { get; set; }
        [StringLength(20)]
        public string PrefixIcon { get; set; }

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
