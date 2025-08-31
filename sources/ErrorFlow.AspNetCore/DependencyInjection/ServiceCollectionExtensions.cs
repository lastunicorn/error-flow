using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrorFlow(this IServiceCollection serviceCollection, Action<ErrorFlowConfiguration> configure)
    {
        ErrorFlowConfiguration configuration = new(serviceCollection);

        if (configure is not null)
            configure(configuration);

        return serviceCollection;
    }
}
