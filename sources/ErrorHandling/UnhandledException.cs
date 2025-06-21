namespace DustInTheWind.AspNetCore.ErrorHandling;

[Serializable]
internal class UnhandledException : Exception
{
    private const string errorMessage = "Unhandled exception. Type: {0}. Ensure that the exception type is registered with the ExceptionsHandler.";

    public UnhandledException(Exception innerException)
        : base(BuildMessage(innerException), innerException)
    {
    }

    private static string BuildMessage(Exception exception)
    {
        string exceptionFullName = exception.GetType().FullName;
        return string.Format(errorMessage, exceptionFullName);
    }
}