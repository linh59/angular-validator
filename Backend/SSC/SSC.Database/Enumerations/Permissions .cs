using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.DataLayer.Enumerations
{
    [Flags]
    public enum Permissions : uint
    {
        None = 0x0,
        // Add new permission above

        Ultimate = 0xFFFFFFFF
    }
}
