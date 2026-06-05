namespace ow_api.Models.Api
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<ApiMessage> Errors { get; set; } = [];
        public List<ApiMessage> Warnings { get; set; } = [];

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>()
            {
                Success = true,
                Data = data
            };
        }

        public static ApiResponse<T> Fail(params string[] errors)
        {
            return new ApiResponse<T>()
            {
                Success = false,
                Errors = errors.Select(error => new ApiMessage() { Message = error }).ToList()
            };
        }
    }
}
