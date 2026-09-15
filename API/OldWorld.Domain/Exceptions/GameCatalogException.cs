using OldWorld.Domain.CustomExceptions;
namespace OldWorld.Domain.Exceptions
{
    public class GameCatalogException : CustomException
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
