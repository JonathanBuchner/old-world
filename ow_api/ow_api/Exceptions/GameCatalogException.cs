namespace ow_api.Exceptions
{
    public class GameCatalogException : ApiException
    {
        public GameCatalogException()
        {
        }

        public GameCatalogException(string message)
            : base(message)
        {
        }

        public GameCatalogException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
