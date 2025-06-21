using Microsoft.AspNetCore.Http;

namespace DustInTheWind.AspNetCore.ErrorHandling;

public interface IHttpErrorResult<in T>
    where T : Exception
{
    Task ExecuteAsync(HttpContext context, T ex);
}