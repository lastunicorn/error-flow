using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace DustInTheWind.AspNetCore.ErrorHandling;

internal class ExceptionsHandler
{
    private readonly ErrorResultTypeCollection errorResultTypes = new();

    public IHttpErrorResult<Exception> DefaultErrorHandler { get; internal set; }

    public void AddRange(IEnumerable<(Type, Type)> errorResultTypes)
    {
        foreach ((Type exceptionType, Type errorResultType) in errorResultTypes)
            this.errorResultTypes.Add(exceptionType, errorResultType);
    }

    public void Add(Type exceptionType, Type errorResultType)
    {
        errorResultTypes.Add(exceptionType, errorResultType);
    }

    public Task Handle<T>(HttpContext context, T exception)
        where T : Exception
    {
        Type errorResultType = errorResultTypes.GetErrorResultType(exception);

        if (errorResultType is not null)
        {
            object errorResultObject = Activator.CreateInstance(errorResultType);

            if (errorResultObject != null)
            {
                MethodInfo executeMethodInfo = errorResultType.GetMethod(nameof(IHttpErrorResult<T>.ExecuteAsync));

                if (executeMethodInfo != null)
                    return (Task)executeMethodInfo.Invoke(errorResultObject, [context, exception]);
            }
        }

        if (DefaultErrorHandler is not null)
            return DefaultErrorHandler.ExecuteAsync(context, exception);

        throw new UnhandledException(exception);
    }
}