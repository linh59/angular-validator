using SSC.Core.Base.Entity.Abstraction;
using System.Collections.Generic;

namespace SSC.Database.Entity
{
    public class Comment : BaseGuidEntity
    {
        public string Content { get; set; }
        public virtual ICollection<UploadedFile> UploadedFiles { get; set; } = new List<UploadedFile>();
    }
}
