using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

// Default error handler for testing AddDefaultErrorHandler<T>() method
public class DefaultTestErrorHandler : IErrorHandler<Exception>
{
    public Task Handle(HttpContext context, Exception ex)
    {
        return Task.CompletedTask;
    }
}