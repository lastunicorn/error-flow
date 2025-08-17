using DustInTheWind.ErrorFlow.AspNetCore.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.ErrorFlowConfigurationTests;

public class ConstructorTests
{
    private readonly IServiceCollection serviceCollection;

    public ConstructorTests()
    {
        serviceCollection = new ServiceCollection();
    }

    [Fact]
    public void Constructor_WithValidServiceCollection_ShouldInitializeCorrectly()
    {
        // Act
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Assert
        Assert.NotNull(configuration);
        Assert.Single(serviceCollection.Where(s => s.ServiceType == typeof(ErrorHandlingEngine)));
    }

    [Fact]
    public void Constructor_WithNullServiceCollection_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new ErrorFlowConfiguration(null));
        Assert.Equal("serviceCollection", exception.ParamName);
    }

    [Fact]
    public void Constructor_ShouldRegisterErrorHandlingEngineAsSingleton()
    {
        // Act
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Assert
        var engineDescriptor = serviceCollection.Single(s => s.ServiceType == typeof(ErrorHandlingEngine));
        Assert.Equal(ServiceLifetime.Singleton, engineDescriptor.Lifetime);
    }
}
