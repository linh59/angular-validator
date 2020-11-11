using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Arguments.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
        
    }
}
