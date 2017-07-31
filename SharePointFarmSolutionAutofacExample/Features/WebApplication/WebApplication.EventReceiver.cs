using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using SharePointFarmSolutionAutofacExample.HttpModules;

namespace SharePointFarmSolutionAutofacExample.Features.WebApplication
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("03a0a479-bebf-4b0d-8bd7-feb8e568f4d9")]
    public class WebApplicationEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = (SPWebApplication) properties.Feature.Parent;

            webApp.WebConfigModifications.Add(
                new SPWebConfigModification(typeof(AutofacContainerDisposalModule).FullName, "configuration/system.web/httpModules")
                {
                    Value = $"<add name=\"{typeof(AutofacContainerDisposalModule).FullName}\" type=\"{typeof(AutofacContainerDisposalModule).AssemblyQualifiedName}\" />",
                    Owner = GetType().FullName,
                    Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode
                });
            webApp.WebConfigModifications.Add(
                new SPWebConfigModification(typeof(AutofacPropertyInjectionModule).FullName, "configuration/system.web/httpModules")
                {
                    Value = $"<add name=\"{typeof(AutofacPropertyInjectionModule).FullName}\" type=\"{typeof(AutofacPropertyInjectionModule).AssemblyQualifiedName}\" />",
                    Owner = GetType().FullName,
                    Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode
                });
            webApp.WebConfigModifications.Add(
                new SPWebConfigModification(typeof(AutofacContainerDisposalModule).FullName, "configuration/system.webServer/modules")
                {
                    Value = $"<add name=\"{typeof(AutofacContainerDisposalModule).FullName}\" type=\"{typeof(AutofacContainerDisposalModule).AssemblyQualifiedName}\" preCondition=\"managedHandler\" />",
                    Owner = GetType().FullName,
                    Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode
                });
            webApp.WebConfigModifications.Add(
                new SPWebConfigModification(typeof(AutofacPropertyInjectionModule).FullName, "configuration/system.webServer/modules")
                {
                    Value = $"<add name=\"{typeof(AutofacPropertyInjectionModule).FullName}\" type=\"{typeof(AutofacPropertyInjectionModule).AssemblyQualifiedName}\" preCondition=\"managedHandler\" />",
                    Owner = GetType().FullName,
                    Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode
                });

            webApp.Update();
            webApp.WebService.ApplyWebConfigModifications();
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = (SPWebApplication)properties.Feature.Parent;

            for (var i = webApp.WebConfigModifications.Count - 1; i >= 0; i--)
            {
                if (webApp.WebConfigModifications[i].Owner == GetType().FullName)
                {
                    webApp.WebConfigModifications.Remove(webApp.WebConfigModifications[i]);
                }
            }

            webApp.Update();
            webApp.WebService.ApplyWebConfigModifications();
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
