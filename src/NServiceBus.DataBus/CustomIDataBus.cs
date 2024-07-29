namespace NServiceBus.Features;

using Microsoft.Extensions.DependencyInjection;
using NServiceBus.DataBus;

class CustomIDataBus : Feature
{
    public CustomIDataBus()
    {
        DependsOn<DataBusFeature>();
    }

    protected override void Setup(FeatureConfigurationContext context)
    {
        var customDataBusDefinition = context.Settings.Get<DataBusDefinition>(DataBusFeature.SelectedDataBusKey) as CustomDataBus;

        context.Services.AddSingleton(sp => customDataBusDefinition.DataBusFactory(sp));
    }
}