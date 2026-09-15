namespace OldWorld.Infrastructure.Imports.Exceptions
{
    public class OldWorldBuilderJsonDeserializeException : ImportException
    {
        public List<string> Errors { get; set; } = [];

        public OldWorldBuilderJsonDeserializeException()
        {
        }

        public OldWorldBuilderJsonDeserializeException(string message)
            : base(message)
        {
            Errors = [message];
        }

        public OldWorldBuilderJsonDeserializeException(List<string> errors)
            : base("Old World Builder JSON could not be deserialized.")
        {
            Errors = errors;
        }

        public OldWorldBuilderJsonDeserializeException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = [message];
        }
    }
}
