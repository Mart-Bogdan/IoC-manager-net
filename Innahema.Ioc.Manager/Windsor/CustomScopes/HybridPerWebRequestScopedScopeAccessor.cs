using System;
using System.Web;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace Innahema.Ioc.Manager.Windsor.CustomScopes
{
    public class HybridPerWebRequestScopedScopeAccessor : IScopeAccessor
    {
        private readonly IScopeAccessor _webRequestScopeAccessor = (IScopeAccessor)new WebRequestScopeAccessor();
       
        public ILifetimeScope GetScope(CreationContext context)
        {
            CallContextLifetimeScope contextLifetimeScope = CallContextLifetimeScope.ObtainCurrentScope();
            if (contextLifetimeScope != null)
                return (ILifetimeScope)contextLifetimeScope;
            if (HttpContext.Current != null && PerWebRequestLifestyleModuleUtils.IsInitialized)
                return this._webRequestScopeAccessor.GetScope(context);
            throw new InvalidOperationException("Naither Http nor CallContext scope was available. Did you forget to call container.BeginScope()?");
        }

        public void Dispose()
        {

            CallContextLifetimeScope contextLifetimeScope = CallContextLifetimeScope.ObtainCurrentScope();
            if (contextLifetimeScope == null)
            {
                this._webRequestScopeAccessor.Dispose();
                return;
            }
            contextLifetimeScope.Dispose();
        }
    }
}