using System;

namespace ow_gen_lib.CustomExceptions
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
