using System;

namespace OWP_LIB.Exceptions
{
    internal class InvalidPairingStateException : CustomOwpLibException
    {
        public InvalidPairingStateException()
        {
        }

        public InvalidPairingStateException(string message)
            : base(message)
        {
        }

        public InvalidPairingStateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
