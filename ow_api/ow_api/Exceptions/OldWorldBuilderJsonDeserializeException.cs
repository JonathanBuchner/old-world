using ow_api.Models.Api;

namespace ow_api.Exceptions
{
    public class OldWorldBuilderJsonDeserializeException : ApiException
    {
        public List<ApiMessage> Errors { get; set; } = [];

        public OldWorldBuilderJsonDeserializeException()
        {
        }

        public OldWorldBuilderJsonDeserializeException(string message)
            : base(message)
        {
            Errors = [new ApiMessage() { Message = message }];
        }

        public OldWorldBuilderJsonDeserializeException(List<ApiMessage> errors)
            : base("Old World Builder JSON could not be deserialized.")
        {
            Errors = errors;
        }

        public OldWorldBuilderJsonDeserializeException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = [new ApiMessage() { Message = message }];
        }
    }
}
