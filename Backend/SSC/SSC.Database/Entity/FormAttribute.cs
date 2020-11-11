using SSC.Core.Base.Entity.Abstraction;

namespace SSC.Database.Entity
{
    public class FormAttribute : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
