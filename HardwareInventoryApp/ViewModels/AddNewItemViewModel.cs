using HardwareInventoryService.Models.Models;
using HardwareInventoryService.Models.Models.Enums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HardwareInventoryApp.ViewModels
{
    public partial class AddNewItemViewModel : Window
    {
        private List<Categories> categories;
        private byte[] pictureByteArray;
        private byte[] pdfByteArray;

        public AddNewItemViewModel()
        {
            InitializeComponent();
            this.InitCategories();
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

        private void AddPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                this.pictureByteArray = File.ReadAllBytes(openFileDialog.FileName);
                this.PictureName.Text = Path.GetFileName(openFileDialog.FileName);
            }
        }

        public byte[] GetPicture()
        {
            return this.pictureByteArray;
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
            }
        }

        public byte[] GetDocument()
        {
            return this.pdfByteArray;
        }

        private void AddNewItem(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
