using Caliburn.Micro;
using HardwareInventoryApp.Helpers;
using HardwareInventoryService.Models.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HardwareInventoryApp.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        private readonly IWindowManager _windowManager;

        private ImageSource secondPicture;

        public ImageSource SecondPicture
        {
            get { return secondPicture; }
            set { secondPicture = value; }
        }

        public MainWindowViewModel(IWindowManager windowManager)
        {
            this._windowManager = windowManager;
            this.SecondPicture = this.ToImage(Data.Session.AccountPhoto);
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
