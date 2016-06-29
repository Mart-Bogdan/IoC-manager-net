using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Innahema.Ioc.Common.Attributes;
using Innahema.Ioc.Manager.Core;

namespace Innahema.Ioc.Manager.Windsor.Installers
{
    public abstract class BaseInstaller
    {
        protected IEnumerable<Type> AttributeServiceSelector(Type t, Type[] bases)
        {
            return t.GetCustomAttributes<BindsToAttribute>().Select(attr => attr.TypeToBind);
        }

        //////////////////////////////////////


        protected IEnumerable<Type> GetTypesFromThisApplication()
        {
            String path;
            if (System.Web.HttpContext.Current != null)
            {
                path = System.Web.HttpContext.Current.Server.MapPath(@"~/bin");
            }
            else
            {
                path = Directory.GetCurrentDirectory();
            }

            var typesFromThisApplication = 
                Directory.EnumerateFiles(path, string.Format("{0}*.dll", IocManager.BaseName), SearchOption.TopDirectoryOnly)
                .Concat(
                    Directory.EnumerateFiles(path, string.Format("{0}*.exe", IocManager.BaseName), SearchOption.TopDirectoryOnly)
                    )
                .Select(s =>
                {
                    try
                    {
                        return Assembly.ReflectionOnlyLoadFrom(s);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(a=>a!=null)
                .Select(asm=>asm.FullName)
                .Select(s =>
                {
                    try
                    {
                        return Assembly.Load(s);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(a=>a!=null)
                //.Where(a => a.FullName.StartsWith("Samuel"))
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.IsClass && !typeof(Attribute).IsAssignableFrom(t) && !t.IsEnum && !t.IsValueType)
                .ToList();
            return typesFromThisApplication;
        }

        protected virtual string GetInterfaceName(Type @interface)
        {
            string name = @interface.Name;
            if (name.Length > 1 && (int)name[0] == 73 && char.IsUpper(name[1]))
                return name.Substring(1);
            return name;
        }

        protected Type[] GetServiceTypes(Type type)
        {
            Type[] interfaces;
            var bindsToSelfAttribute = type.GetCustomAttribute<BindsToSelfAttribute>();
            var bindsToAttribute = type.GetCustomAttribute<BindsToAttribute>();

            if (bindsToSelfAttribute != null)
            {
                interfaces = new[] { type };
            }
            else if (bindsToAttribute != null)
            {
                interfaces = new[] { bindsToAttribute.TypeToBind };
            }
            else
            {
                interfaces = type
                    .GetAllInterfaces()
                    .Where(@interface
                        => type.Name.Contains(GetInterfaceName(@interface))
                    ).ToArray();
            }
            return interfaces;
        }

        protected static void Register(
            IWindsorContainer container,
            Type @interface, 
            Type type,
            Func<ComponentRegistration<object>, ComponentRegistration<object>> lifestyleSelector 
            )
        {
            container.
                Register(
                    lifestyleSelector(Component.For(@interface).ImplementedBy(type))
                );
        }
    }
}
