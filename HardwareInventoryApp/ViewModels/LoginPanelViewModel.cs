using Autofac;
using Caliburn.Micro;
using HardwareInventoryApp.IoCContainer;
using HardwareInventoryService.Models.Models.Authorization;
using HardwareInventoryService.ServicesReferences.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareInventoryApp.ViewModels
{
    public class LoginPanelViewModel : Screen
    {
        private readonly IAuthorizationService _authorizationService;

        private readonly IWindowManager _windowManager;

        private string login;

        private string password;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public LoginPanelViewModel(IWindowManager windowManager)
        {
            this._authorizationService = ContainerConfig._container.Resolve<IAuthorizationService>();
            this._windowManager = windowManager;
        }

        public void CloseApp()
        {
            this.TryClose();
        }

        public void LogIn()
        {
            var newSession = new Session();
            newSession.Username = this.Login;
            newSession.Password = this.Password;

            try
            {
                var test = this._authorizationService.Authorize(newSession);

                var mainWindowViewModel = ContainerConfig._container.Resolve<MainWindowViewModel>();
                
                this._windowManager.ShowWindow(mainWindowViewModel);

                this.TryClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
