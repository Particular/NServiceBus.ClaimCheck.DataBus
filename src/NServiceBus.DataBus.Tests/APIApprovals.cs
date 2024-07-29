﻿namespace NServiceBus.DataBus.Tests;

using NUnit.Framework;
using Particular.Approvals;
using PublicApiGenerator;

[TestFixture]
public class APIApprovals
{
    [Test]
    public void ApproveAzureStorageQueueTransport()
    {
        var publicApi = typeof(DataBus).Assembly.GeneratePublicApi(new ApiGeneratorOptions
        {
            ExcludeAttributes = ["System.Runtime.Versioning.TargetFrameworkAttribute", "System.Reflection.AssemblyMetadataAttribute"]
        });
        Approver.Verify(publicApi);
    }
}