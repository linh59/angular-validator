using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Entity.Interfaces
{
    public interface IRole<T> : IIdentity<T> where T : IComparable
    {
        public string Name { get; set; }
        public byte[] Permission { get; set; }
        public int Hienrachy { get; set; }
    }
    public interface IRole : IRole<int>, IIdentity<int> { }
}
