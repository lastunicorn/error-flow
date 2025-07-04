using DustInTheWind.ErrorFlow.AspNetCore.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DustInTheWind.ErrorFlow.AspNetCore;

public class ErrorFlowConfiguration
{
    private readonly IServiceCollection serviceCollection;
    private readonly ErrorHandlingEngine engine;

    public ErrorFlowConfiguration(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
        engine = new ErrorHandlingEngine();

        serviceCollection.AddSingleton(engine);
    }

    public ErrorFlowConfiguration AddErrorHandlersFromAssembly(params Assembly[] assemblies)
    {
        IEnumerable<(Type, Type)> errorHandlers = assemblies
            .Where(x => x is not null)
            .SelectMany(x => x.GetTypes())
            .SelectMany(EnumerateErrorHandlerTypes);

        foreach ((Type errorType, Type errorHandlerType) in errorHandlers)
        {
            engine.AddHandler(errorType, errorHandlerType);
            serviceCollection.AddTransient(errorHandlerType);
        }

        return this;
    }

    public ErrorFlowConfiguration AddDefaultErrorHandler(Type defaultErrorHandlerType)
    {
        engine.DefaultErrorHandlerType = defaultErrorHandlerType;

        return this;
    }

    private static IEnumerable<(Type, Type)> EnumerateErrorHandlerTypes(Type type)
    {
        Type errorHandlerInterfaceType = typeof(IErrorHandler<>);

        IEnumerable<Type> implementedInterfaces = type.GetInterfaces()
            .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == errorHandlerInterfaceType);

        foreach (Type implementedInterface in implementedInterfaces)
        {
            Type exceptionType = implementedInterface.GetGenericArguments()[0];
            yield return (exceptionType, type);
        }
    }
}