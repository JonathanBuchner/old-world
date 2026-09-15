namespace OldWorld.Infrastructure.Imports.Exceptions
{
    public class InvalidJsonListException : ImportException
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
