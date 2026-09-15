using OldWorld.Domain.CustomExceptions;
using System;

namespace OldWorld.Calculator.Exceptions
{
    public class InvalidProfileCustomException : CustomException
    {
        public InvalidProfileCustomException()
        {
        }

        public InvalidProfileCustomException(string message)
            : base(message)
        {
        }

        public InvalidProfileCustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
