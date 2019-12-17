using Caliburn.Micro;
using HardwareInventoryApp.Helpers;
using HardwareInventoryApp.Views;
using HardwareInventoryService.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInventoryApp.ViewModels
{
    public class ItemsViewModel : PropertyChangedBase
    {
        private List<Item> listOfItems;

        public List<Item> ListOfItems
        {
            get { return listOfItems; }
            set { listOfItems = value; NotifyOfPropertyChange(() => ListOfItems); }
        }

        public ItemsViewModel()
        {
            this.ListOfItems = Data.Items;
        }

        public void AddNewItem()
        {
            var addNewItemViewModel = new AddNewItemView();
            addNewItemViewModel.ShowDialog();
        }
    }
}
