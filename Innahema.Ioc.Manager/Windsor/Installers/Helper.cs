using System;
using Bender.Reflection;
using Castle.MicroKernel.Registration;
using Innahema.Ioc.Common.Attributes;

namespace Innahema.Ioc.Manager.Windsor.Installers
{
    internal static class Helper
    {
        public static ComponentRegistration<T> ConfigIsDefaultImpl<T>(this ComponentRegistration<T> cr, Type type)
            where T : class
        {
            if (type.HasAttribute<DefaultImplementation>())
            {
                return cr.IsDefault();
            }

            return cr;
        }
    }
}