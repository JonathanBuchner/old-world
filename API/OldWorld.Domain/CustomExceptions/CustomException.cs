using System;

namespace OldWorld.Domain.CustomExceptions
{
    public abstract class CustomException : Exception
    {
        protected CustomException()
        {
        }

        protected CustomException(string message)
            : base(message)
        {
        }

        protected CustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
