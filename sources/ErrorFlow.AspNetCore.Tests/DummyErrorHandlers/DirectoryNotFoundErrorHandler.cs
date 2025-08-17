using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

public class DirectoryNotFoundErrorHandler : IErrorHandler<DirectoryNotFoundException>
{
    public Task Handle(HttpContext context, DirectoryNotFoundException ex)
    {
        return Task.CompletedTask;
    }
}
