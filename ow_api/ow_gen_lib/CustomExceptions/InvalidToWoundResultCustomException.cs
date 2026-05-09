using System;

namespace ow_gen_lib.CustomExceptions
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
