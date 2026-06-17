using SkyRoute.Api.Middleware;

namespace SkyRoute.Api.Shared.Extensions
{
    public static class ErrorHandlingMiddlewareExtensions
    {
        // Extension method used to add the middleware to the HTTP request pipeline.
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
