using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Base.Entity.Interfaces
{
    public interface IOwnedRecord<T> where T: IComparable
    {
        public T Creator { get; set; }
    }
}
