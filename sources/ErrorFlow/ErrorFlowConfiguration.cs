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

    public ErrorFlowConfiguration AddErrorHandlersFromAssemblyContaining(Type type)
    {
        Assembly assembly = type?.Assembly
            ?? throw new ArgumentNullException(nameof(type), "Type cannot be null.");

        return AddErrorHandlersFromAssembly(assembly);
    }

    public ErrorFlowConfiguration AddErrorHandlersFromAssemblyContaining<T>()
    {
        Assembly assembly = typeof(T).Assembly;
        return AddErrorHandlersFromAssembly(assembly);
    }

    public ErrorFlowConfiguration AddErrorHandlersFromAssembly(params Assembly[] assemblies)
    {
        if (assemblies == null)
            return this;

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

    public ErrorFlowConfiguration AddDefaultErrorHandler<T>()
        where T : class, IErrorHandler<Exception>
    {
        engine.DefaultErrorHandlerType = typeof(T);

        return this;
    }

    public ErrorFlowConfiguration UseExplicitMode(bool useExplicitMode = true)
    {
        engine.UseExplicitMode = useExplicitMode;
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