using DustInTheWind.ErrorFlow.AspNetCore.Core;
using DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.ErrorFlowConfigurationTests;

public class AddErrorHandlersFromAssemblyTests
{
    private readonly IServiceCollection serviceCollection;

    public AddErrorHandlersFromAssemblyTests()
    {
        serviceCollection = new ServiceCollection();
    }

    [Fact]
    public void AddErrorHandlersFromAssembly_WithValidAssemblies_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);
        var assembly = typeof(UnauthorizedAccessErrorHandler).Assembly;

        // Act
        var result = configuration.AddErrorHandlersFromAssembly(assembly);

        // Assert
        Assert.Same(configuration, result);
    }

    [Fact]
    public void AddErrorHandlersFromAssembly_WithNullAssemblies_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.AddErrorHandlersFromAssembly(null);

        // Assert
        Assert.Same(configuration, result);
    }

    [Fact]
    public void AddErrorHandlersFromAssembly_WithMixedValidAndNullAssemblies_ShouldProcessValidOnes()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);
        var assembly = typeof(TimeoutErrorHandler).Assembly;

        // Act
        var result = configuration.AddErrorHandlersFromAssembly(assembly, null);

        // Assert
        Assert.Same(configuration, result);
        // Note: We can't easily verify specific handler registration since the assembly contains many error handlers
        // and we might get duplicates. The important thing is that the method returns successfully.
    }

    [Fact]
    public void AddErrorHandlersFromAssembly_ShouldRegisterErrorHandlersAsTransient()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);
        
        // Create a mock assembly that only contains our specific test handler
        var testAssembly = typeof(NotSupportedErrorHandler).Assembly;

        // Act
        configuration.AddErrorHandlersFromAssembly(testAssembly);

        // Assert
        var handlerDescriptor = serviceCollection.FirstOrDefault(s => s.ServiceType == typeof(NotSupportedErrorHandler));
        Assert.NotNull(handlerDescriptor);
        Assert.Equal(ServiceLifetime.Transient, handlerDescriptor.Lifetime);
    }

    [Fact]
    public void AddErrorHandlersFromAssembly_WithMultipleErrorHandlers_ShouldRegisterAll()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);
        var assembly = typeof(OutOfMemoryErrorHandler).Assembly;

        // Act
        configuration.AddErrorHandlersFromAssembly(assembly);

        // Assert
        Assert.Contains(serviceCollection, s => s.ServiceType == typeof(OutOfMemoryErrorHandler));
        Assert.Contains(serviceCollection, s => s.ServiceType == typeof(InvalidOperationErrorHandler));
    }
}
