using System;
using Innahema.Ioc.Common.Interfaces;

namespace Innahema.Ioc.Manager.Core
{
    public interface IIocFacade
    {
        void Stop();
        void Start();
        IServiceContainer<T> GetServiceContainer<T>();
        void PreApplicationStartMethod();
        void BindSingleton<T>(T service) where T : class;
        IDisposable BeginScope();
    }
}
