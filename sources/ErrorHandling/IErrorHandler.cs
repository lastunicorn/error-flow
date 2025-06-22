using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorHandling.AspNetCore;

public interface IErrorHandler<in T>
    where T : Exception
{
    Task Handle(HttpContext context, T ex);
}