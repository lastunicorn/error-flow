using DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.ErrorFlowConfigurationTests;

public class AddDefaultErrorHandlerTests
{
    private readonly IServiceCollection serviceCollection;

    public AddDefaultErrorHandlerTests()
    {
        serviceCollection = new ServiceCollection();
    }

    [Fact]
    public void AddDefaultErrorHandler_WithValidType_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.AddDefaultErrorHandler(typeof(DefaultTestErrorHandler));

        // Assert
        Assert.Same(configuration, result);
    }

    [Fact]
    public void AddDefaultErrorHandler_Generic_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.AddDefaultErrorHandler<DefaultTestErrorHandler>();

        // Assert
        Assert.Same(configuration, result);
    }
}
