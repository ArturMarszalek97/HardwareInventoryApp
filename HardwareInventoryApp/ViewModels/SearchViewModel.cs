using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using HardwareInventoryService.Models.Models;
using System.Windows.Data;

namespace HardwareInventoryApp.ViewModels
{
    public class SearchViewModel : Caliburn.Micro.PropertyChangedBase
    {
        public ObservableCollection<Item> itemsList = null;

        public SearchViewModel()
        {
            this.PrepareList();
        }

        private void PrepareList()
        {
            this.itemsList = new ObservableCollection<Item>();

            foreach (var item in Helpers.Data.Items)
            {
                this.itemsList.Add(item);
            }

            //this.searchList.ItemsSource = this.itemsList;

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.searchList.ItemsSource);
        }
    }
}
