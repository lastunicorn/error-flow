using DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.ErrorFlowConfigurationTests;

public class AddErrorHandlersFromAssemblyContainingTests
{
    private readonly IServiceCollection serviceCollection;

    public AddErrorHandlersFromAssemblyContainingTests()
    {
        serviceCollection = new ServiceCollection();
    }

    [Fact]
    public void AddErrorHandlersFromAssemblyContaining_WithValidType_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.AddErrorHandlersFromAssemblyContaining(typeof(FileNotFoundErrorHandler));

        // Assert
        Assert.Same(configuration, result);
    }

    [Fact]
    public void AddErrorHandlersFromAssemblyContaining_WithNullType_ShouldThrowArgumentNullException()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            configuration.AddErrorHandlersFromAssemblyContaining(null));
        Assert.Equal("type", exception.ParamName);
        Assert.Contains("Type cannot be null.", exception.Message);
    }

    [Fact]
    public void AddErrorHandlersFromAssemblyContaining_Generic_ShouldReturnSameInstance()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration.AddErrorHandlersFromAssemblyContaining<DirectoryNotFoundErrorHandler>();

        // Assert
        Assert.Same(configuration, result);
    }
}
