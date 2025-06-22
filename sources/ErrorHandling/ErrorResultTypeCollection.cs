namespace DustInTheWind.AspNetCore.ErrorHandling;

internal class ErrorResultTypeCollection
{
    private readonly Dictionary<Type, Type> types = [];

    public void Add(Type exceptionType, Type errorResultType)
    {
        ArgumentNullException.ThrowIfNull(exceptionType);
        ArgumentNullException.ThrowIfNull(errorResultType);

        if (!IsExceptionType(exceptionType))
            throw new ArgumentException($"The type {exceptionType.FullName} must inherit from System.Exception.", nameof(exceptionType));

        bool isErrorResponseType = IsErrorResultType(exceptionType, errorResultType);
        if (!isErrorResponseType)
            throw new ArgumentException($"The type {errorResultType.FullName} must implement IHttpErrorResult<{exceptionType.Name}>.", nameof(errorResultType));

        types.Add(exceptionType, errorResultType);
    }

    private static bool IsExceptionType(Type exceptionType)
    {
        return exceptionType.IsSubclassOf(typeof(Exception)) || exceptionType == typeof(Exception);
    }

    private static bool IsErrorResultType(Type exceptionType, Type errorResultType)
    {
        Type interfaceType = typeof(IHttpErrorResult<>).MakeGenericType(exceptionType);
        return interfaceType.IsAssignableFrom(errorResultType);
    }

    public Type GetErrorResultType<T>(T exception)
    {
        bool success = types.TryGetValue(exception.GetType(), out Type errorResultType);

        return success
            ? errorResultType
            : null;
    }
}
