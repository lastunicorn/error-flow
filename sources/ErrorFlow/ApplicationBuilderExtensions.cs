using Microsoft.AspNetCore.Builder;

namespace DustInTheWind.ErrorFlow.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseErrorFlow(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ErrorFlowMiddleware>();
    }
}
