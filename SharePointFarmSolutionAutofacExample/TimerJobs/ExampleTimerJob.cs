using System;
using Autofac;
using Microsoft.SharePoint.Administration;

namespace SharePointFarmSolutionAutofacExample.TimerJobs
{
    public class ExampleTimerJob : SPJobDefinition
    {
        public ExampleTimerJob()
        {
        }

        public ExampleTimerJob(string name, SPWebApplication webApplication)
            : base(name, webApplication, null, SPJobLockType.Job)
        {
            Title = name;
        }

        public override void Execute(Guid targetInstanceId)
        {
            using (ILifetimeScope scope = AutofacContainerProviderAccessor.Instance.ContainerProvider.ApplicationContainer.BeginLifetimeScope(
                builder =>
                {
                    // Add any other TimerJob scoped Autofac registrations here, e.g. SPWebApplication, SPSite, SPWeb etc.
                    // If you use instance not created by you, remember to call ExternallyOwned()
                    // For example:
                    // builder.Register(_ => WebApplication).AsSelf().ExternallyOwned();
                }))
            {
                // We are injecting properties to current TimerJob instance,
                // you can resolve some object from container instead
                scope.InjectProperties(this);
            }
        }
    }
}
