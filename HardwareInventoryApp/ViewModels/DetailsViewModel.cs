using HardwareInventoryService.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HardwareInventoryApp.ViewModels
{
    public partial class DetailsViewModel : Window
    {
        public DetailsViewModel(Item item)
        {
            InitializeComponent();
            this.FillTheFields(item);
        }

        private void FillTheFields(Item item)
        {
            this.ItemName.Text = item.ItemName;
            this.DateOfPurchase.Text = item.DateOfPurchaseToDisplay;
            this.Price.Text = $"{item.Price} zł";
            this.Category.Text = item.Category;
            this.Shop.Text = item.Shop;
            this.Warranty.Text = item.WarrantyToDisplay;
            this.Return.Text = item.ReturnToDisplay;
            this.Note.Text = item.Note;
            this.PictureName.Text = item.PictureName;
            this.DocumentName.Text = item.PDFDocumentName;
        }
    }
}
