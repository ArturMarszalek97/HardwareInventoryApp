using Autofac;
using Caliburn.Micro;
using HardwareInventoryApp.ViewModels;
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
            builder.RegisterType<CacheService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<LoginPanelViewModel>().SingleInstance();
            builder.RegisterType<MainWindowViewModel>().SingleInstance();
            builder.RegisterType<ContextMenuViewModel>().SingleInstance();
            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();

            _container = builder.Build();
        }
    }
}
