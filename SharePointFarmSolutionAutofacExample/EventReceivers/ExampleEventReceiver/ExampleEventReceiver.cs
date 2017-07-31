using Autofac;
using Microsoft.SharePoint;

namespace SharePointFarmSolutionAutofacExample.EventReceivers.ExampleEventReceiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ExampleEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);

            using (ILifetimeScope scope = AutofacContainerProviderAccessor.Instance.ContainerProvider.ApplicationContainer.BeginLifetimeScope(
                builder =>
                {
                    // Add any other event scoped Autofac registrations here, e.g. SPSite, SPWeb etc.
                    // If you use instance not created by you, remember to call ExternallyOwned()
                    // For example:
                    // builder.Register(_ => properties.Web).AsSelf().ExternallyOwned();
                }))
            {
                // SomeType service = scope.Resolve<SomeType>();
                // service.DoSomething();
            }
        }
    }
}
