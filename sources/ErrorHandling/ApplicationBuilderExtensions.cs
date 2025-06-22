using Microsoft.AspNetCore.Builder;

namespace DustInTheWind.ErrorHandling.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
