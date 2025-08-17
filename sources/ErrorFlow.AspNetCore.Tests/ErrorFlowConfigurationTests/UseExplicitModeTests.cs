using DustInTheWind.ErrorFlow.AspNetCore.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.ErrorFlowConfigurationTests;

public class UseExplicitModeTests
{
    private readonly IServiceCollection serviceCollection;

    public UseExplicitModeTests()
    {
        serviceCollection = new ServiceCollection();
    }

    [Fact]
    public void UseExplicitMode_WithTrue_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.UseExplicitMode(true);

        // Assert
        Assert.Same(configuration, result);
    }

    [Fact]
    public void UseExplicitMode_WithFalse_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.UseExplicitMode(false);

        // Assert
        Assert.Same(configuration, result);
    }

    [Fact]
    public void UseExplicitMode_WithDefaultParameter_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.UseExplicitMode();

        // Assert
        Assert.Same(configuration, result);
    }
}
