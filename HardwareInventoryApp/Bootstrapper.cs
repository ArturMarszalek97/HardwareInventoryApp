using Autofac;
using Caliburn.Micro;
using HardwareInventoryApp.Helpers;
using HardwareInventoryApp.IoCContainer;
using HardwareInventoryApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HardwareInventoryApp
{
    public class Bootstrapper : BootstrapperBase
    {
        private static IContainer Container;

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(PasswordBoxHelper.BoundPasswordProperty, "Password", "PasswordChanged");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            
            DisplayRootViewFor<LoginPanelViewModel>();
        }

        protected override void Configure()
        {
            ContainerConfig.Configure();
            Container = ContainerConfig._container;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container.IsRegistered(service))
                    return Container.Resolve(service);
            }
            else
            {
                if (Container.IsRegisteredWithKey(key, service))
                {
                    return Container.ResolveKeyed(key, service);
                }
            }
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }
        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }
    }
}
