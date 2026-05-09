using System;

namespace ow_gen_lib.CustomExceptions
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
