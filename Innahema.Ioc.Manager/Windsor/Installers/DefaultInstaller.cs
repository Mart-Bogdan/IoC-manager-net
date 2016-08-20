using System.Linq;
using System.Reflection;
using Bender.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Innahema.Ioc.Common.Attributes.Abstract;

namespace Innahema.Ioc.Manager.Windsor.Installers
{
    public class DefaultInstaller : BaseInstaller, IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var allTypes = base.GetTypesFromThisApplication();
            foreach (var type in allTypes.Where(t => !t.GetCustomAttributes().OfType<BaseIoCAttribute>().Any()))
            {
                var type1 = type;
                var interfaces = GetServiceTypes(type);
                foreach (var @interface in interfaces)
                {
                    Register(container, @interface, type, x => x.LifestyleTransient().ConfigIsDefaultImpl(type1));
                }
            }
        }
    }
}
