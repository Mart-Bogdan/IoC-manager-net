using System.Linq;
using System.Web.Mvc;
using Bender.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Innahema.Ioc.Manager.Windsor.Installers
{
    public class ControllerInstaller : BaseInstaller, IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var allTypes = base.GetTypesFromThisApplication();
            foreach (var type in allTypes.Where(t => t.CanBeCastTo<IController>()))
            {
                Register(container, type, type, x => x.LifestyleTransient());
                
            }
        }
    }
}