using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

public class NotSupportedErrorHandler : IErrorHandler<NotSupportedException>
{
    public Task Handle(HttpContext context, NotSupportedException ex)
    {
        return Task.CompletedTask;
    }
}
