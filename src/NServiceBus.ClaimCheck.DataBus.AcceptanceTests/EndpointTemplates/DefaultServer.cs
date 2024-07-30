namespace NServiceBus.ClaimCheck.DataBus.AcceptanceTests.EndpointTemplates;

using System;
using System.Threading.Tasks;
using AcceptanceTesting.Customization;
using AcceptanceTesting.Support;

public class DefaultServer : IEndpointSetupTemplate
{
    public async Task<EndpointConfiguration> GetConfiguration(RunDescriptor runDescriptor, EndpointCustomizationConfiguration endpointConfiguration, Func<EndpointConfiguration, Task> configurationBuilderCustomization)
    {
        var builder = new EndpointConfiguration(endpointConfiguration.EndpointName);
        builder.EnableInstallers();

        builder.Recoverability()
            .Delayed(delayed => delayed.NumberOfRetries(0))
            .Immediate(immediate => immediate.NumberOfRetries(0));
        builder.SendFailedMessagesTo("error");

        await builder.DefineTransport(new ConfigureEndpointAcceptanceTestingTransport(true, true), runDescriptor, endpointConfiguration).ConfigureAwait(false);

        builder.UseSerialization<SystemJsonSerializer>();

        await configurationBuilderCustomization(builder).ConfigureAwait(false);

        // scan types at the end so that all types used by the configuration have been loaded into the AppDomain
        builder.ScanTypesForTest(endpointConfiguration);

        return builder;
    }
}
