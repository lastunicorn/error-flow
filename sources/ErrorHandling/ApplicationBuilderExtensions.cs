using Microsoft.AspNetCore.Builder;

namespace DustInTheWind.AspNetCore.ErrorHandling;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseErrorHandlers(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
