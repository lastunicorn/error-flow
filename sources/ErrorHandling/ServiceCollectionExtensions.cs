using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DustInTheWind.AspNetCore.ErrorHandling;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrorResults(this IServiceCollection serviceCollection, Action<ExceptionsHandlerOptions> createOptions)
    {
        ExceptionsHandlerOptions options = new();

        if (createOptions is not null)
            createOptions(options);

        ExceptionsHandler handler = new();

        if (options.Assemblies is not null)
        {
            IEnumerable<(Type, Type)> errorResults = options.Assemblies
                .Where(x => x is not null)
                .SelectMany(EnumerateErrorResults);

            if (errorResults is not null)
                handler.AddRange(errorResults);
        }

        if (options.DefaultErrorResult is not null)
            handler.DefaultErrorHandler = options.DefaultErrorResult;

        serviceCollection.AddSingleton(handler);
        return serviceCollection;
    }

    public static IServiceCollection AddErrorResults(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        IEnumerable<(Type, Type)> errorResults = assemblies
            .SelectMany(EnumerateErrorResults);

        ExceptionsHandler handler = new();
        handler.AddRange(errorResults);

        serviceCollection.AddSingleton(handler);
        return serviceCollection;
    }

    public static IServiceCollection AddErrorResults(this IServiceCollection serviceCollection, Assembly assembly)
    {
        IEnumerable<(Type, Type)> errorResults = assembly.EnumerateErrorResults();

        ExceptionsHandler handler = new();
        handler.AddRange(errorResults);

        serviceCollection.AddSingleton(handler);
        return serviceCollection;
    }

    private static IEnumerable<(Type, Type)> EnumerateErrorResults(this Assembly assembly)
    {
        Type handlerInterfaceType = typeof(IHttpErrorResult<>);

        foreach (Type type in assembly.GetTypes())
        {
            IEnumerable<Type> implementedInterfaces = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == handlerInterfaceType);

            foreach (Type implementedInterface in implementedInterfaces)
            {
                Type exceptionType = implementedInterface.GetGenericArguments()[0];
                yield return (exceptionType, type);
            }
        }
    }
}
