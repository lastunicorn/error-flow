namespace DustInTheWind.ErrorFlow.AspNetCore;

public class MayThrowAttribute : Attribute
{
    public Type ErrorType { get; }

    public MayThrowAttribute(Type exceptionType)
    {
        ErrorType = exceptionType ?? throw new ArgumentNullException(nameof(exceptionType));
    }
}