using DustInTheWind.ErrorFlow.AspNetCore.Core;
using DustInTheWind.ErrorFlow.AspNetCore.Helpers;
using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore;

internal class ErrorFlowMiddleware
{
    private readonly RequestDelegate next;
    private readonly ErrorHandlingEngine errorHandlingEngine;

    public ErrorFlowMiddleware(RequestDelegate next, ErrorHandlingEngine errorHandlingEngine)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.errorHandlingEngine = errorHandlingEngine ?? throw new ArgumentNullException(nameof(errorHandlingEngine));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await errorHandlingEngine.Handle(context, ex);
        }
    }
}
