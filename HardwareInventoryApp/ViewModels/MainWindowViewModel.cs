using Caliburn.Micro;
using HardwareInventoryApp.Helpers;
using HardwareInventoryService.Models.Models.Authorization;
using HardwareInventoryService.ServicesReferences.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HardwareInventoryApp.ViewModels
{
    public class MainWindowViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;

        private readonly ICacheService _cacheService;

        private ImageSource accountPhoto;

        public ImageSource AccountPhoto
        {
            get { return accountPhoto; }
            set { accountPhoto = value; }
        }

        public MainWindowViewModel(IWindowManager windowManager, ICacheService cacheService)
        {
            this._windowManager = windowManager;
            this._cacheService = cacheService;
            this.AccountPhoto = this.ToImage(Data.Session.AccountPhoto);

            this.GetData();

            

            var itemsVM = new ItemsViewModel();
            ActivateItem(itemsVM);
        }

        private void GetData()
        {
            Data.Items = this._cacheService.GetItems().ToList();

            foreach (var item in Data.Items)
            {
                item.DateOfPurchaseToDisplay = item.DateOfPurchase.ToShortDateString();
            }
        }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
