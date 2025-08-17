using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;

// Unique test error handlers to avoid conflicts during test execution
public class FileNotFoundErrorHandler : IErrorHandler<FileNotFoundException>
{
    public Task Handle(HttpContext context, FileNotFoundException ex)
    {
        return Task.CompletedTask;
    }
}
