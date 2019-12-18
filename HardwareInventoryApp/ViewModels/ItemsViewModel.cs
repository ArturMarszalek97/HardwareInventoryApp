using Caliburn.Micro;
using HardwareInventoryApp.Helpers;
using HardwareInventoryApp.Views;
using HardwareInventoryService.Models.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HardwareInventoryApp.ViewModels
{
    public class ItemsViewModel : PropertyChangedBase
    {
        private BindableCollection<Item> listOfItems;

        private ICommand _command;

        public BindableCollection<Item> ListOfItems
        {
            get { return listOfItems; }
            set { listOfItems = value; NotifyOfPropertyChange(() => ListOfItems); }
        }

        public ItemsViewModel()
        {
            this.ListOfItems = new BindableCollection<Item>();
            foreach (var item in Data.Items)
            {
                this.ListOfItems.Add(item);
            }
        }

        public ICommand Command
        {
            get
            {
                return _command ?? (_command = new RelayCommand(x =>
                {
                    DoStuff(x as Item);
                }));
            }
        }

        private void DoStuff(Item item)
        {
            MessageBox.Show(item.ItemName + " element clicked");
        }

        public void AddNewItem()
        {
            var addNewItemViewModel = new AddNewItemViewModel();

            if (addNewItemViewModel.ShowDialog() == true)
            {
                var item = new Item()
                {
                    ItemName = addNewItemViewModel.ItemName.Text,
                    DateOfPurchase = addNewItemViewModel.DateOfPurchase.SelectedDate.Value,
                    Price = Convert.ToInt64(addNewItemViewModel.Price.Text),
                    Category = addNewItemViewModel.Category.Text,
                    Shop = addNewItemViewModel.Shop.Text,
                    Note = addNewItemViewModel.Note.Text
                };

                var selectedCategory = (Categories)addNewItemViewModel.Category.SelectedItem;

                item.Category = selectedCategory.CategoryName;
                item.ImageSource = selectedCategory.ImagePath;

                item.UserID = Data.Session.UserId;

                item.DateOfPurchaseToDisplay = addNewItemViewModel.DateOfPurchase.SelectedDate.Value.ToShortDateString();

                if (addNewItemViewModel.IsLifetime.IsChecked == true)
                {
                    item.WarrantyToDisplay = "Dożywotnia";
                }
                else
                {
                    item.Warranty = Convert.ToInt32(addNewItemViewModel.Warranty.Value);
                    item.WarrantyToDisplay = addNewItemViewModel.Warranty.Value.ToString();
                }

                if (addNewItemViewModel.WithoutLimits.IsChecked == true)
                {
                    item.ReturnToDisplay = "Bez limitów";
                }
                else
                {
                    item.Return = Convert.ToInt32(addNewItemViewModel.Return.Value);
                    item.ReturnToDisplay = addNewItemViewModel.Return.Value.ToString();
                }

                item.Picture = addNewItemViewModel.GetPicture();

                item.PDFDocument = addNewItemViewModel.GetDocument();

                this.listOfItems.Add(item);
            }
        }
    }
}
