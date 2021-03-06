﻿using Autofac;
using Caliburn.Micro;
using HardwareInventoryApp.Helpers;
using HardwareInventoryService.Models.Models;
using HardwareInventoryService.ServicesReferences.Contracts;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HardwareInventoryApp.ViewModels
{
    public class ItemsViewModel : PropertyChangedBase
    {
        private BindableCollection<Item> listOfItems;

        private ICommand _command;
        private Item selectedItem;
        private ICommand _remove;
        private ICommand _edit;
        private ICommand _details;
        private ICacheService _cacheService;

        private ICommand _addNewItem;

        public ICommand AddItem
        {
            get { return _addNewItem; }
            set { _addNewItem = value; }
        }


        public ICommand Details
        {
            get { return _details; }
            set { _details = value; }
        }


        public ICommand Edit
        {
            get { return _edit; }
            set { _edit = value; }
        }


        public ICommand Remove
        {
            get { return _remove; }
            set { _remove = value; }
        }

        public BindableCollection<Item> ListOfItems
        {
            get { return listOfItems; }
            set { listOfItems = value; NotifyOfPropertyChange(() => ListOfItems); }
        }

        public ItemsViewModel()
        {
            this._cacheService = IoCContainer.ContainerConfig._container.Resolve<ICacheService>();
            this.InitRelayCommands();
            this.ListOfItems = new BindableCollection<Item>();
            foreach (var item in Data.Items)
            {
                this.ListOfItems.Add(item);
            }
        }

        private void InitRelayCommands()
        {
            this.AddItem = new RelayCommand(async (x) => await this.AddNewItem());
            this.Remove = new RelayCommand(async (x) => await this.RemoveItem());
            this.Edit = new RelayCommand(async (x) => await this.EditItem());
            this.Details = new RelayCommand(x => this.ShowDetails());
        }

        public ICommand Command
        {
            get
            {
                return _command ?? (_command = new RelayCommand(x =>
                {
                    SetSelectedItem(x as Item);
                }));
            }
        }

        private void SetSelectedItem(Item item)
        {
            this.selectedItem = item;
        }

        public async Task AddNewItem()
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

                //var maxItemID = 0;
                //foreach (var itemInList in this.ListOfItems)
                //{
                //    if (itemInList.ItemID > maxItemID)
                //    {
                //        maxItemID = itemInList.ItemID;
                //    }
                //}

                //item.ItemID = maxItemID + 1;

                item.KeyForCache = Guid.NewGuid();

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
                item.PictureName = addNewItemViewModel.GetPictureName();

                item.PDFDocument = addNewItemViewModel.GetDocument();
                item.PDFDocumentName = addNewItemViewModel.GetDocumentName();

                this.listOfItems.Add(item);

                try
                {
                    await this._cacheService.AddItemAsync(item);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private void ShowDetails()
        {
            var detailsViewModel = new DetailsViewModel(this.selectedItem);

            detailsViewModel.Show();
        }

        private async Task EditItem()
        {
            if (this.selectedItem == null)
            {
                MessageBox.Show("Nie wybrano żadnego elementu z listy!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var editItemViewModel = new EditItemViewModel(this.selectedItem);

            if (editItemViewModel.ShowDialog() == true)
            {
                var editedItem = new Item()
                {
                    ItemName = editItemViewModel.ItemName.Text,
                    DateOfPurchase = editItemViewModel.DateOfPurchase.SelectedDate.Value,
                    Price = Convert.ToInt64(editItemViewModel.Price.Text),
                    Category = editItemViewModel.Category.Text,
                    Shop = editItemViewModel.Shop.Text,
                    Note = editItemViewModel.Note.Text
                };

                var selectedCategory = (Categories)editItemViewModel.Category.SelectedItem;

                editedItem.Category = selectedCategory.CategoryName;
                editedItem.ImageSource = selectedCategory.ImagePath;

                editedItem.UserID = Data.Session.UserId;

                editedItem.DateOfPurchaseToDisplay = editItemViewModel.DateOfPurchase.SelectedDate.Value.ToShortDateString();

                if (editItemViewModel.IsLifetime.IsChecked == true)
                {
                    editedItem.WarrantyToDisplay = "Dożywotnia";
                }
                else
                {
                    editedItem.Warranty = Convert.ToInt32(editItemViewModel.Warranty.Value);
                    editedItem.WarrantyToDisplay = editItemViewModel.Warranty.Value.ToString();
                }

                if (editItemViewModel.WithoutLimits.IsChecked == true)
                {
                    editedItem.ReturnToDisplay = "Bez limitów";
                }
                else
                {
                    editedItem.Return = Convert.ToInt32(editItemViewModel.Return.Value);
                    editedItem.ReturnToDisplay = editItemViewModel.Return.Value.ToString();
                }

                editedItem.Picture = editItemViewModel.GetPicture();
                editedItem.PictureName = editItemViewModel.GetPictureName();

                if (string.IsNullOrEmpty(editedItem.PictureName) || editedItem.Picture == null)
                {
                    editedItem.Picture = this.selectedItem.Picture;
                    editedItem.PictureName = this.selectedItem.PictureName;
                }

                editedItem.PDFDocument = editItemViewModel.GetDocument();
                editedItem.PDFDocumentName = editItemViewModel.GetDocumentName();

                if (string.IsNullOrEmpty(editedItem.PDFDocumentName) || editedItem.PDFDocument == null)
                {
                    editedItem.PDFDocument = this.selectedItem.PDFDocument;
                    editedItem.PDFDocumentName = this.selectedItem.PDFDocumentName;
                }

                editedItem.ItemID = this.selectedItem.ItemID;
                editedItem.KeyForCache = this.selectedItem.KeyForCache;

                this.ListOfItems.Remove(this.selectedItem);
                this.listOfItems.Add(editedItem);

                await this._cacheService.UpdateItemAsync(editedItem);
            }
        }

        private async Task RemoveItem()
        {
            if (this.selectedItem == null)
            {
                MessageBox.Show("Nie wybrano żadnego elementu z listy!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Czy na pewno chcesz usunąć przedmiot: {this.selectedItem.ItemName}?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.ListOfItems.Remove(this.selectedItem);
                await this._cacheService.RemoveItemAsync(this.selectedItem);
            }
        }
    }
}
