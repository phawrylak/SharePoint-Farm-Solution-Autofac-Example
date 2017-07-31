using System.Web;
using Autofac.Integration.Web;

namespace SharePointFarmSolutionAutofacExample.HttpModules
{
    public class AutofacContainerDisposalModule : ContainerDisposalModule
    {
        public override void Init(HttpApplication context)
        {
            ContainerProviderAccessor = AutofacContainerProviderAccessor.Instance;
            base.Init(context);
        }
    }
}
