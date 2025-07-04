using DustInTheWind.ErrorFlow.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection serviceCollection, Action<ErrorHandlingConfiguration> configure)
    {
        ErrorHandlingConfiguration configuration = new(serviceCollection);

        if (configure is not null)
            configure(configuration);

        return serviceCollection;
    }
}
