using System.Reflection;
using DustInTheWind.ErrorFlow.AspNetCore.Helpers;
using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Core;

internal class ErrorHandlingEngine
{
    private readonly ErrorHandlerTypeCollection errorHandlerTypes = new();

    public Type DefaultErrorHandlerType { get; internal set; }

    public bool UseExplicitMode { get; set; }

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
        Type errorHandlerType = ChooseErrorHandlerType(context, error);

        object errorHandlerObject = context.RequestServices.GetService(errorHandlerType)
            ?? throw new UnhandledErrorException(error);

        MethodInfo executeMethodInfo = errorHandlerType.GetMethod(nameof(IErrorHandler<T>.Handle))
            ?? throw new UnhandledErrorException(error);

        return (Task)executeMethodInfo.Invoke(errorHandlerObject, [context, error]);
    }

    private Type ChooseErrorHandlerType<T>(HttpContext context, T error)
        where T : Exception
    {
        bool isErrorAllowed = !UseExplicitMode || IsErrorAllowed(context, error);

        Type errorHandlerType = isErrorAllowed
            ? errorHandlerTypes.GetErrorHandlerType(error)
            : DefaultErrorHandlerType;

        return errorHandlerType is null
            ? throw new UnhandledErrorException(error)
            : errorHandlerType;
    }

    private static bool IsErrorAllowed<T>(HttpContext context, T error)
        where T : Exception
    {
        Type errorType = error.GetType();
        Endpoint endpoint = context.GetEndpoint();

        return endpoint.GetAttributes<MayThrowAttribute>()
            .Any(x => x.ErrorType == errorType);
    }
}
