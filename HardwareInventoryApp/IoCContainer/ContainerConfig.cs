using Autofac;
using HardwareInventoryService.ServicesReferences.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInventoryApp.IoCContainer
{
    public static class ContainerConfig
    {
        public static IContainer _container;

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LoggerService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AuthorizationService>().AsImplementedInterfaces().SingleInstance();

            _container = builder.Build();
        }
    }
}
