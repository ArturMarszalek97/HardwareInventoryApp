using HardwareInventoryService.Models.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareInventoryApp.ViewModels
{
    public partial class EditItemViewModel : Window
    {
        private byte[] pictureByteArray;
        private byte[] pdfByteArray;

        public EditItemViewModel()
        {
            InitializeComponent();
        }

        public EditItemViewModel(Item item)
        {
            InitializeComponent();
            this.FillTheFields(item);
        }

        private void FillTheFields(Item item)
        {
            this.ItemName.Text = item.ItemName;
            this.DateOfPurchase.SelectedDate = item.DateOfPurchase;
            this.Price.Text = item.Price.ToString();
            this.Category.SelectedItem = item.Category;
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
            }
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

        private void ConfirmChanges(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
