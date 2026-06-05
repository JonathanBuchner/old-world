namespace ow_api.Exceptions
{
    public class InvalidJsonListException : ApiException
    {
        public InvalidJsonListException()
        {
        }

        public InvalidJsonListException(string message)
            : base(message)
        {
        }

        public InvalidJsonListException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
