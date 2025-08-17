using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

public class UnauthorizedAccessErrorHandler : IErrorHandler<UnauthorizedAccessException>
{
    public Task Handle(HttpContext context, UnauthorizedAccessException ex)
    {
        return Task.CompletedTask;
    }
}
