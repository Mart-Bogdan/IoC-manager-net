using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Innahema.Ioc.Common.Interfaces;
using Innahema.Ioc.Manager.Windsor.ServiceLocation;

namespace Innahema.Ioc.Manager.Windsor.Installers
{
    public class ScopedGetterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IScopedServiceLocator>().ImplementedBy<ScopedServiceLocator>().LifestyleTransient());
        }
    }
}
