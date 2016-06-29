using System;
using Innahema.Ioc.Common.Interfaces;
using Innahema.Ioc.Manager.Windsor;

namespace Innahema.Ioc.Manager.Core
{
    public static class IocManager
    {
        private static IIocFacade _impl = GetImplementation();

        private static IIocFacade GetImplementation()
        {
            return new WindsorFacade();
        }


        public static IServiceContainer<T> GetServiceContainer<T>()
        {
            return _impl.GetServiceContainer<T>();
        }

        public static void Start()
        {
            _impl.Start();
        }
        public static void PreApplicationStartMethod()
        {
            _impl.PreApplicationStartMethod();
        }

        public static void Stop()
        {
            _impl.Stop();
        }

        public static void BindSingleton<T>(T service) where T : class
        {
            _impl.BindSingleton(service);
        }

        public static IDisposable BeginScope()
        {
            return _impl.BeginScope();
        }

        public static string BaseName = "None";
    }
}






