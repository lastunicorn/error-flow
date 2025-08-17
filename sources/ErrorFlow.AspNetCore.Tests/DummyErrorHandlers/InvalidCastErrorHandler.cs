using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

public class InvalidCastErrorHandler : IErrorHandler<InvalidCastException>
{
    public Task Handle(HttpContext context, InvalidCastException ex)
    {
        return Task.CompletedTask;
    }
}
