using System;
using Innahema.Ioc.Common.Interfaces;
using Innahema.Ioc.Manager.Core;

namespace Innahema.Ioc.Manager.Windsor.CustomScopes
{
    public class CustomScopeMangement : ICustomScopeMangement
    {
        public IDisposable BeginScope()
        {
            return IocManager.BeginScope();
        }
    }
}