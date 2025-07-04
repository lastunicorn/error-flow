using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore;

public interface IErrorHandler<in T>
    where T : Exception
{
    Task Handle(HttpContext context, T ex);
}