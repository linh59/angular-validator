using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSC.Core.Base.Entity.Interfaces
{
    public interface IPermission<T> : IIdentity<T> where T : IComparable
    {
        public string Name { get; set; }
        public string Group { get; set; }
        [Range(1, int.MaxValue)]
        public int GroupIdentifier { get; set; }
        public byte PermissionIdentifier { get; set; } 
    }
}
