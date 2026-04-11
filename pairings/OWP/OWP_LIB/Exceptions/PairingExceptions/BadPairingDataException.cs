using System;

namespace OWP_LIB.Exceptions
{
    internal class BadPairingDataException : CustomOwpLibException
    {
        public BadPairingDataException()
        {
        }

        public BadPairingDataException(string message)
            : base(message)
        {
        }

        public BadPairingDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
