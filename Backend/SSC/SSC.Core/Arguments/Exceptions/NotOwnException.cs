using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Arguments.Exceptions
{
    public class NotOwnException : Exception
    {
        public NotOwnException() : base("")
        {
        }

        public NotOwnException(string message)
            : base(message)
        {
        }

        public NotOwnException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
