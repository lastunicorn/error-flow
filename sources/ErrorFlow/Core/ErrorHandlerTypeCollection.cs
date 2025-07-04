namespace DustInTheWind.ErrorFlow.AspNetCore.Core;

internal class ErrorHandlerTypeCollection
{
    private static readonly Type BaseErrorType = typeof(Exception);

    private readonly Dictionary<Type, Type> types = [];

    public void Add(Type errorType, Type errorHandlerType)
    {
        ArgumentNullException.ThrowIfNull(errorType);
        ArgumentNullException.ThrowIfNull(errorHandlerType);

        if (!IsErrorType(errorType))
            throw new ArgumentException($"The type {errorType.FullName} must inherit from {BaseErrorType.FullName}.", nameof(errorType));

        bool isErrorHandlerType = IsErrorHandlerType(errorHandlerType, errorType);
        if (!isErrorHandlerType)
            throw new ArgumentException($"The type {errorHandlerType.FullName} must implement IErrorHandler<{errorType.Name}>.", nameof(errorHandlerType));

        types.Add(errorType, errorHandlerType);
    }

    public Type GetErrorHandlerType<T>(T error)
    {
        Type errorType = error.GetType();
        bool success = types.TryGetValue(errorType, out Type errorHandlerType);

        return success
            ? errorHandlerType
            : null;
    }

    private static bool IsErrorType(Type errorType)
    {
        return errorType.IsSubclassOf(BaseErrorType) || errorType == BaseErrorType;
    }

    private static bool IsErrorHandlerType(Type errorHandlerType, Type exceptionType)
    {
        Type interfaceType = typeof(IErrorHandler<>).MakeGenericType(exceptionType);
        return interfaceType.IsAssignableFrom(errorHandlerType);
    }
}
