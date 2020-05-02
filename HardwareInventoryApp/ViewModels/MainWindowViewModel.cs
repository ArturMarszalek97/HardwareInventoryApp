using Caliburn.Micro;
using HardwareInventoryApp.Helpers;
using HardwareInventoryService.Models.Models.Authorization;
using HardwareInventoryService.ServicesReferences.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HardwareInventoryApp.ViewModels
{
    public class MainWindowViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;

        private readonly ICacheService _cacheService;

        private ImageSource accountPhoto;

        private System.Windows.Controls.ListViewItem _selectedOption;

        public System.Windows.Controls.ListViewItem SelectedOption
        {
            get { return _selectedOption; }
            set { _selectedOption = value; NotifyOfPropertyChange(() => SelectedOption); this.ActivateUserControl(value); }
        }

        private void ActivateUserControl(ListViewItem value)
        {
            if (value == null)
                return;

            switch (value.Name)
            {
                case "listViewItem":
                    {
                        var itemsVM = new ItemsViewModel();
                        ActivateItem(itemsVM);
                    } break;
                case "listViewItem1":
                    {
                        var searchVM = new SearchViewModel();
                        ActivateItem(searchVM);
                    } break;
                case "listViewItem2":
                    {
                        //var statisticsVM = new StatisticsViewModel();
                        //ActivateItem(statisticsVM);

                        Window window = new Views.StatisticView();
                        window.Show();
                    } break;
                default:
                    break;
            }
        }

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
                
                switch(item.Category)
                {
                    case "Elektronika":
                        {
                            item.ImageSource = "/Assets/CategoryIcons/electronic.png";
                        } break;
                    case "Biuro":
                        {
                            item.ImageSource = "/Assets/CategoryIcons/office.png";
                        } break;
                    case "Motoryzacja":
                        {
                            item.ImageSource = "/Assets/CategoryIcons/motorization.png";
                        } break;
                    case "Dom":
                        {
                            item.ImageSource = "/Assets/CategoryIcons/home.png";
                        } break;
                    case "AGD":
                        {
                            item.ImageSource = "/Assets/CategoryIcons/agd.png";
                        } break;
                    case "Zdrowie":
                        {
                            item.ImageSource = "/Assets/CategoryIcons/health.png";
                        } break;
                    case "Inne":
                        {
                            item.ImageSource = "/Assets/CategoryIcons/office.png";
                        } break;
                }
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
