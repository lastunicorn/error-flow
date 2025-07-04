using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace DustInTheWind.ErrorFlow.AspNetCore.Core;

internal class ErrorHandlingEngine
{
    private readonly ErrorHandlerTypeCollection errorHandlerTypes = new();

    public Type DefaultErrorHandlerType { get; internal set; }

    public void AddHandlers(IEnumerable<(Type, Type)> errorHandlerTypes)
    {
        foreach ((Type errorType, Type errorHandlerType) in errorHandlerTypes)
            this.errorHandlerTypes.Add(errorType, errorHandlerType);
    }

    public void AddHandler(Type errorType, Type errorHandlerType)
    {
        errorHandlerTypes.Add(errorType, errorHandlerType);
    }

    public Task Handle<T>(HttpContext context, T error)
        where T : Exception
    {
        Type errorHandlerType = errorHandlerTypes.GetErrorHandlerType(error)
            ?? DefaultErrorHandlerType
            ?? throw new UnhandledErrorException(error);

        object errorHandlerObject = Activator.CreateInstance(errorHandlerType)
            ?? throw new UnhandledErrorException(error);

        MethodInfo executeMethodInfo = errorHandlerType.GetMethod(nameof(IErrorHandler<T>.Handle))
            ?? throw new UnhandledErrorException(error);

        return (Task)executeMethodInfo.Invoke(errorHandlerObject, [context, error]);
    }
}
