using DustInTheWind.ErrorFlow.AspNetCore.Tests.DummyErrorHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace DustInTheWind.ErrorFlow.AspNetCore.Tests.ErrorFlowConfigurationTests;

public class FluentInterfaceTests
{
    private readonly IServiceCollection serviceCollection;

    public FluentInterfaceTests()
    {
        serviceCollection = new ServiceCollection();
    }

    [Fact]
    public void FluentInterface_ShouldAllowMethodChaining()
    {
        // Arrange
        var configuration = new ErrorFlowConfiguration(serviceCollection);

        // Act
        var result = configuration
            .AddErrorHandlersFromAssemblyContaining<InvalidCastErrorHandler>()
            .AddDefaultErrorHandler<DefaultTestErrorHandler>()
            .UseExplicitMode(true);

        // Assert
        Assert.Same(configuration, result);
    }
}
