using System;

namespace OWP_LIB.Exceptions
{
    internal abstract class CustomOwpLibException : Exception
    {
        protected CustomOwpLibException()
        {
        }

        protected CustomOwpLibException(string message)
            : base(message)
        {
        }

        protected CustomOwpLibException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
