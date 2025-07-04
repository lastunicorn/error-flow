using DustInTheWind.ErrorFlow.AspNetCore.Core;
using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore;

internal class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ErrorHandlingEngine exceptionHandler;

    public ErrorHandlingMiddleware(RequestDelegate next, ErrorHandlingEngine exceptionHandler)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await exceptionHandler.Handle(context, ex);
        }
    }
}
