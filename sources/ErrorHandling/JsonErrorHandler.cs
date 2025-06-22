using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace DustInTheWind.ErrorHandling.AspNetCore;

public abstract class JsonErrorHandler<TException, TResponseBody> : IErrorHandler<TException>
    where TException : Exception
{
    protected abstract HttpStatusCode HttpStatusCode { get; }

    public Task Handle(HttpContext context, TException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode;

        TResponseBody response = BuildHttpResponseBody(ex);

        return response is null
            ? Task.CompletedTask
            : context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    protected abstract TResponseBody BuildHttpResponseBody(TException ex);
}
