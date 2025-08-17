using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

public class OutOfMemoryErrorHandler : IErrorHandler<OutOfMemoryException>
{
    public Task Handle(HttpContext context, OutOfMemoryException ex)
    {
        return Task.CompletedTask;
    }
}