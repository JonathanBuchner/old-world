namespace ow_api.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected ApiException()
        {
        }

        protected ApiException(string message)
            : base(message)
        {
        }

        protected ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
