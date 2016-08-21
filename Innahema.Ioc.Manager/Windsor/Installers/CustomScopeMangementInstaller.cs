using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Innahema.Ioc.Common.Interfaces;
using Innahema.Ioc.Manager.Windsor.CustomScopes;

namespace Innahema.Ioc.Manager.Windsor.Installers
{
    public class CustomScopeMangementInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICustomScopeMangement>().ImplementedBy<CustomScopeMangement>().LifestyleTransient());
        }
    }
}