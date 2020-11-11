using SSC.Core.Base.Entity.Abstraction;
using SSC.Core.Base.Entity.Interfaces;

namespace SSC.Database.Entity
{
    public class UploadedFile : BaseGuidEntity, IOwnedRecord<string>
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string FileExtensions { get; set; }
        public string Creator { get; set; }
    }
}
