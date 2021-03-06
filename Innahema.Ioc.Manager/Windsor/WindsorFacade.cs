﻿using System;
using System.Configuration;
using System.Web.Mvc;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using Innahema.Ioc.Common.Interfaces;
using Innahema.Ioc.Manager.Core;
using Innahema.Ioc.Manager.Windsor.Factories;

namespace Innahema.Ioc.Manager.Windsor
{
    class WindsorFacade : IIocFacade
    {
        private IWindsorContainer _container;
        public void Start()
        {
            if (ConfigurationManager.GetSection("castle") != null)
            {
                _container = new Castle.Windsor.WindsorContainer(new XmlInterpreter());
            }
            else
            {
                _container = new Castle.Windsor.WindsorContainer();
            }
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            _container.Install(FromAssembly.This());

            //костыль!  Вкрутить фаьрику для конфигов
            //_container.Register(Component.For<IActiveMqConfig>().UsingFactoryMethod((k, iface) => (IActiveMqConfig)ConfigurationManager.GetSection("activemq")));
            //_container.Register(Component.For<IKernel>().UsingFactoryMethod(x => _container.Kernel));
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public void PreApplicationStartMethod()
        {

        }

        public void BindSingleton<T>(T service)
            where T:class
        {
            _container.Kernel.Register(Component.For<T>().LifestyleSingleton().Instance(service));
        }

        public IDisposable BeginScope()
        {
            return _container.BeginScope();
        }

        public IServiceContainer<T> GetServiceContainer<T>()
        {
            return _container.Resolve<IScopedServiceGetter>().GetService<T>();
        }

        public  void Stop()
        {
            _container.Dispose();
        }
    }
}
