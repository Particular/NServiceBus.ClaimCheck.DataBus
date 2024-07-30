namespace NServiceBus.ClaimCheck.DataBus.AcceptanceTests.EndpointTemplates;

using Configuration.AdvancedExtensibility;
using System.Threading.Tasks;
using NServiceBus.AcceptanceTesting.Support;

public static class ConfigureExtensions
{
    public static RoutingSettings ConfigureRouting(this EndpointConfiguration configuration) => new(configuration.GetSettings());

    public static async Task DefineTransport(this EndpointConfiguration config, IConfigureEndpointTestExecution transportConfiguration, RunDescriptor runDescriptor, EndpointCustomizationConfiguration endpointCustomizationConfiguration)
    {
        await transportConfiguration.Configure(endpointCustomizationConfiguration.EndpointName, config, runDescriptor.Settings, endpointCustomizationConfiguration.PublisherMetadata);
        runDescriptor.OnTestCompleted(_ => transportConfiguration.Cleanup());
    }
}