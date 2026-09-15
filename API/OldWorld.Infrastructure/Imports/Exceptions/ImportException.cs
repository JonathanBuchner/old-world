namespace OldWorld.Infrastructure.Imports.Exceptions
{
    public abstract class ImportException : Exception
    {
        protected ImportException()
        {
        }

        protected ImportException(string message)
            : base(message)
        {
        }

        protected ImportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
