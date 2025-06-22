namespace DustInTheWind.ErrorHandling.AspNetCore;

[Serializable]
internal class UnhandledErrorException : Exception
{
    private const string errorMessage = "Unhandled error: {0}. Ensure that a engine is registered with the ErrorHandlingEngine.";

    public UnhandledErrorException(Exception innerException)
        : base(BuildMessage(innerException), innerException)
    {
    }

    private static string BuildMessage(Exception exception)
    {
        string exceptionFullName = exception.GetType().FullName;
        return string.Format(errorMessage, exceptionFullName);
    }
}