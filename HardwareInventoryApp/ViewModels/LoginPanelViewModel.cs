﻿using Autofac;
using Caliburn.Micro;
using HardwareInventoryApp.IoCContainer;
using HardwareInventoryService.Models.Models.Authorization;
using HardwareInventoryService.ServicesReferences.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInventoryApp.ViewModels
{
    public class LoginPanelViewModel : Screen
    {
        private readonly IAuthorizationService _authorizationService;

        private readonly IWindowManager _windowManager;

        public LoginPanelViewModel(IWindowManager windowManager)
        {
            this._authorizationService = ContainerConfig._container.Resolve<IAuthorizationService>();
            this._windowManager = windowManager;

            var newSession = new Session();
            newSession.Username = "testowyUsername";

            var test = this._authorizationService.Authorize(newSession);

            var mvm = new MainWindowViewModel();

            this._windowManager.ShowWindow(mvm);
        }
    }
}
