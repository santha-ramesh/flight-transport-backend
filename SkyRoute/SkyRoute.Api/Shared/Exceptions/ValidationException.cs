namespace SkyRoute.Api.Shared.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, IEnumerable<string> errors): base(message)
        {
            Errors = errors.ToList();
        }

        public List<string> Errors { get; set; } = [];
    }
}
