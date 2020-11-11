using System;
using System.Collections.Generic;
using System.Text;

namespace SSC.Core.Arguments.Exceptions
{
    public class ConflictDateTimeException : Exception
    {
        public ConflictDateTimeException()
        {
        }

        public ConflictDateTimeException(string message)
            : base(message)
        {
        }

        public ConflictDateTimeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
