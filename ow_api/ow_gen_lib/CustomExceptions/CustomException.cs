using System;

namespace ow_gen_lib.CustomExceptions
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
