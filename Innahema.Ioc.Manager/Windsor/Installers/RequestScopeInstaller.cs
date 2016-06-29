using System.Linq;
using Bender.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Innahema.Ioc.Common.Attributes;
using Innahema.Ioc.Manager.Windsor.CustomScopes;

namespace Innahema.Ioc.Manager.Windsor.Installers
{
    public class RequestScopeInstaller : BaseInstaller, IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var allTypes = base.GetTypesFromThisApplication();
            foreach (var type in allTypes.Where(t => t.HasAttribute<RequestScopeAttribute>()))
            {
                var type1 = type;
                var interfaces = GetServiceTypes(type);
                foreach (var @interface in interfaces)
                {
                    Register(container, @interface, type, x => x.LifeStyle.Scoped<HybridPerWebRequestScopedScopeAccessor>().ConfigIsDefaultImpl(type1));
                }
            }
        }
    }
}
