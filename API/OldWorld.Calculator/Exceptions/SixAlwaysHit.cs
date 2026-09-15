using OldWorld.Domain.CustomExceptions;
using System;

namespace OldWorld.Calculator.Exceptions
{
    public class SixAlwaysHit : CustomException
    {
        public SixAlwaysHit()
        {
        }

        public SixAlwaysHit(string message)
            : base(message)
        {
        }

        public SixAlwaysHit(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
