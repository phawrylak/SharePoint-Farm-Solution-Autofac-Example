using Autofac;
using Autofac.Integration.Web;

namespace SharePointFarmSolutionAutofacExample
{
    internal class AutofacContainerProviderAccessor : IContainerProviderAccessor
    {
        public static readonly IContainerProviderAccessor Instance = new AutofacContainerProviderAccessor();
        private static readonly IContainerProvider InternalContainerProvider = GetContainerProvider();

        private AutofacContainerProviderAccessor()
        {
        }

        public IContainerProvider ContainerProvider => InternalContainerProvider;

        private static IContainerProvider GetContainerProvider()
        {
            ContainerBuilder builder = new ContainerBuilder();

            // register dependencies here...

            return new ContainerProvider(builder.Build());
        }
    }
}
