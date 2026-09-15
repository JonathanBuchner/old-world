using OldWorld.Domain.CustomExceptions;
using System;

namespace OldWorld.Calculator.Exceptions
{
    public class InvalidToWoundResultCustomException : CustomException
    {
        public InvalidToWoundResultCustomException()
        {
        }

        public InvalidToWoundResultCustomException(string message)
            : base(message)
        {
        }

        public InvalidToWoundResultCustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
