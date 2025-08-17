using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

// Test error handler that implements multiple interfaces
public class MultipleErrorHandler : IErrorHandler<ArgumentException>, IErrorHandler<ArgumentNullException>
{
    public Task Handle(HttpContext context, ArgumentException ex)
    {
        return Task.CompletedTask;
    }

    public Task Handle(HttpContext context, ArgumentNullException ex)
    {
        return Task.CompletedTask;
    }
}