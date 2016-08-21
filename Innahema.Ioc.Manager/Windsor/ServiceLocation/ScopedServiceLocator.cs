using System;
using Castle.MicroKernel;
using Innahema.Ioc.Common.Interfaces;

namespace Innahema.Ioc.Manager.Windsor.ServiceLocation
{
    public class ScopedServiceLocator : IScopedServiceLocator
    {
        private readonly IKernel _kernel;
        public ScopedServiceLocator(IKernel kernel)
        {
            _kernel = kernel;
        }
        public IServiceContainer<T> GetService<T>()
        {
            return new ServiceContainer<T>(_kernel, _kernel.Resolve<T>());
        }

        public IServiceContainer<T> GetService<T>(Type type)
        {
            return new ServiceContainer<T>(_kernel, (T)_kernel.Resolve(type));
        }

        public IServiceContainer<Object> GetService(Type type)
        {
            return new ServiceContainer<Object>(_kernel, _kernel.Resolve(type));            
        }
    }
}
