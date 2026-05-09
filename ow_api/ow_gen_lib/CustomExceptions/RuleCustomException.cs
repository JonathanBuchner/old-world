using System;

namespace ow_gen_lib.CustomExceptions
{
    public class RuleCustomException : CustomException
    {
        public RuleCustomException()
        {
        }

        public RuleCustomException(string message)
            : base(message)
        {
        }

        public RuleCustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
