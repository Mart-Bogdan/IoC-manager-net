using System;

namespace Innahema.Ioc.Common.Interfaces
{
    public interface IServiceContainer<out T> : IDisposable
    {
        T Service { get; }
    }
}
