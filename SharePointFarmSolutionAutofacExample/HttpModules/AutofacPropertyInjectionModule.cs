using System.Web;
using Autofac.Integration.Web.Forms;

namespace SharePointFarmSolutionAutofacExample.HttpModules
{
    public class AutofacPropertyInjectionModule : PropertyInjectionModule
    {
        public override void Init(HttpApplication context)
        {
            ContainerProviderAccessor = AutofacContainerProviderAccessor.Instance;
            base.Init(context);
        }
    }
}
