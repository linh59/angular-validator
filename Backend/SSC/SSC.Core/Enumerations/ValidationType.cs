using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Enumerations
{
    [Flags]
    public enum ValidationType
    {
        None = 0x000000,
        
        // --Start Group 01
        Required = 0x1,
        Equal = 0x2,
        Greater = 0x4,
        GreaterOrEqual = 0x6,
        Lower = 0x8,
        LowerOrEqual = 0xA,
        // --End Group 01

        // --Start Group 02-05
        StartWith = 0x10,
        Contains = 0x20,
        EndWith = 0x40,
        Email = 0x80,
        Phone = 0x100,
        Link = 0x200,
        Regex = 0x0FFFFF,
        // --End Group 02-05

        Future = 0x100000,
        Past = 0x200000,
        Integer = 0x400000,
        NotRange = 0x800000,
        DayOfWeek = 0x1000000,
    }
}
