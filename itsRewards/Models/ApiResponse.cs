
namespace itsRewards.Models
{
    public class ApiResponse<TResult>
    {
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public ApiError ResponseException { get; set; }
        public TResult Result { get; set; }
    }
}