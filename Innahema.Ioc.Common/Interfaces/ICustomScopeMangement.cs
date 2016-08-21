using System;

namespace Innahema.Ioc.Common.Interfaces
{
    public interface ICustomScopeMangement
    {
        IDisposable BeginScope();
    }
}