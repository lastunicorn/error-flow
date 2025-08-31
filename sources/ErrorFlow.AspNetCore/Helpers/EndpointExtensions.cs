using Microsoft.AspNetCore.Http;

namespace DustInTheWind.ErrorFlow.AspNetCore.Helpers;

internal static class EndpointExtensions
{
    public static TAttribute GetAttribute<TAttribute>(this Endpoint endpoint)
        where TAttribute : class
    {
        ArgumentNullException.ThrowIfNull(endpoint);

        return endpoint.Metadata.GetMetadata<TAttribute>();
    }

    public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Endpoint endpoint)
        where TAttribute : class
    {
        ArgumentNullException.ThrowIfNull(endpoint);

        return endpoint.Metadata.GetOrderedMetadata<TAttribute>();
    }
}
