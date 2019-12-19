using HardwareInventoryService.Models.Models;
using HardwareInventoryService.Models.Models.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace HardwareInventoryApp.ViewModels
{
    public partial class EditItemViewModel : Window
    {
        private byte[] pictureByteArray;
        private byte[] pdfByteArray;
        private string pictureName;
        private string documentName;
        private List<Categories> categories;

        public EditItemViewModel()
        {
            InitializeComponent();
        }

        public EditItemViewModel(Item item)
        {
            InitializeComponent();
            this.InitCategories();
            this.FillTheFields(item);
        }

        private void InitCategories()
        {
            this.categories = new List<Categories>();

            foreach (var item in Enum.GetValues(typeof(ItemCategory)))
            {
                var newCategory = new Categories()
                {
                    CategoryName = item.ToString()
                };

                switch (item.ToString())
                {
                    case "Elektronika":
                        {
                            newCategory.ImagePath = "/Assets/CategoryIcons/electronic.png";
                        }
                        break;
                    case "Biuro":
                        {
                            newCategory.ImagePath = "/Assets/CategoryIcons/office.png";
                        }
                        break;
                    case "Motoryzacja":
                        {
                            newCategory.ImagePath = "/Assets/CategoryIcons/motorization.png";
                        }
                        break;
                    case "Dom":
                        {
                            newCategory.ImagePath = "/Assets/CategoryIcons/home.png";
                        }
                        break;
                    case "AGD":
                        {
                            newCategory.ImagePath = "/Assets/CategoryIcons/agd.png";
                        }
                        break;
                    case "Zdrowie":
                        {
                            newCategory.ImagePath = "/Assets/CategoryIcons/health.png";
                        }
                        break;
                    case "Inne":
                        {
                            newCategory.ImagePath = "/Assets/CategoryIcons/office.png";
                        }
                        break;
                }

                this.categories.Add(newCategory);
            }

            this.Category.ItemsSource = this.categories;
        }

        private void FillTheFields(Item item)
        {
            this.ItemName.Text = item.ItemName;
            this.DateOfPurchase.SelectedDate = item.DateOfPurchase;
            this.Price.Text = item.Price.ToString();

            foreach (Categories comboBoxCategory in this.Category.Items)
            {
                if (comboBoxCategory.CategoryName == item.Category)
                {
                    this.Category.SelectedItem = comboBoxCategory;
                    break;
                }
            }

            this.Shop.Text = item.Shop;

            if (item.WarrantyToDisplay == "Dożywotnia")
            {
                this.IsLifetime.IsChecked = true;
            }
            else
            {
                this.Warranty.Value = item.Warranty;
            }

            if (item.ReturnToDisplay == "")
            {
                this.WithoutLimits.IsChecked = true;
            }
            else
            {
                this.Return.Value = item.Return;
            }

            this.Note.Text = item.Note;
            this.PictureName.Text = item.PictureName;
            this.DocumentName.Text = item.PDFDocumentName;
        }

        private void AddPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                this.pictureByteArray = File.ReadAllBytes(openFileDialog.FileName);
                this.PictureName.Text = Path.GetFileName(openFileDialog.FileName);
                this.pictureName = Path.GetFileName(openFileDialog.FileName);
            }
        }

        public byte[] GetPicture()
        {
            return this.pictureByteArray;
        }

        public string GetPictureName()
        {
            return this.pictureName;
        }

        private void AddDocument(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                this.pdfByteArray = File.ReadAllBytes(openFileDialog.FileName);
                this.DocumentName.Text = Path.GetFileName(openFileDialog.FileName);
                this.documentName = Path.GetFileName(openFileDialog.FileName);
            }
        }

        public byte[] GetDocument()
        {
            return this.pdfByteArray;
        }

        public string GetDocumentName()
        {
            return this.documentName;
        }

        private void ConfirmChanges(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
