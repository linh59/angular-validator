using SSC.Core.Base.Entity.Abstraction;
using SSC.Database.Enumerations;
using System.Collections.Generic;

namespace SSC.Database.Entity
{
    public class SubTask: BaseGuidEntity
    {
        public SubTaskType SubTaskType { get; set; }
        public SubTaskState SubTaskState { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UploadedFile> UploadedFiles { get; set; } = new List<UploadedFile>();
    }
}
