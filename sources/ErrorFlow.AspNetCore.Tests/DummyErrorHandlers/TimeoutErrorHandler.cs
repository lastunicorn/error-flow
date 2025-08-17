using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

public class TimeoutErrorHandler : IErrorHandler<TimeoutException>
{
    public Task Handle(HttpContext context, TimeoutException ex)
    {
        return Task.CompletedTask;
    }
}
