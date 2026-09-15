using System;

namespace OldWorld.Domain.CustomExceptions
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
