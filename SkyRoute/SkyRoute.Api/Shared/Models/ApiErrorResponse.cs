namespace SkyRoute.Api.Shared.Models
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<string>? Errors { get; set; }
    }
}
