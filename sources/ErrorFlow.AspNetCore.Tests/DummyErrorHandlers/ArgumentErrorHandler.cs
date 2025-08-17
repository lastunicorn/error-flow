using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

// Test error handlers for testing purposes
public class ArgumentErrorHandler : IErrorHandler<ArgumentException>
{
    public Task Handle(HttpContext context, ArgumentException ex)
    {
        return Task.CompletedTask;
    }
}
