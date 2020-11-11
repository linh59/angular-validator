using SSC.Core.Base.Entity.Abstraction;

namespace SSC.Database.Entity
{
    public class FormTemplateAttribute : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
