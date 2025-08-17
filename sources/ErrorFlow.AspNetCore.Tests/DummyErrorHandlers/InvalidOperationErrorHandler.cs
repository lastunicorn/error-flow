using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

public class InvalidOperationErrorHandler : IErrorHandler<InvalidOperationException>
{
    public Task Handle(HttpContext context, InvalidOperationException ex)
    {
        return Task.CompletedTask;
    }
}
