using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using HardwareInventoryService.Models.Models;

namespace HardwareInventoryApp.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public ObservableCollection<Item> itemsList = null;

        public SearchView()
        {
            InitializeComponent();
            this.PrepareList();
        }

        private void PrepareList()
        {
            this.itemsList = new ObservableCollection<Item>();

            foreach (var item in Helpers.Data.Items)
            {
                this.itemsList.Add(item);
            }

            this.searchList.ItemsSource = this.itemsList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.searchList.ItemsSource);

            view.Filter = UserFilter;
        }

        private bool UserFilter(object obj)
        {
            if (string.IsNullOrEmpty(txtFilter.Text) && string.IsNullOrEmpty(ShopFilter.Text))
                return true;

            if (string.IsNullOrEmpty(txtFilter.Text) == false && string.IsNullOrEmpty(ShopFilter.Text))
                return ((obj as Item).ItemName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) == 0);

            if (string.IsNullOrEmpty(ShopFilter.Text) == false && string.IsNullOrEmpty(txtFilter.Text))
                return ((obj as Item).Shop.IndexOf(ShopFilter.Text, StringComparison.OrdinalIgnoreCase) == 0);

            return false;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFilter.Text.Length > 0)
            {
                this.ShopFilter.IsReadOnly = true;
            }
            else
            {
                this.ShopFilter.IsReadOnly = false;
            }

            CollectionViewSource.GetDefaultView(this.searchList.ItemsSource).Refresh();
        }

        private void ShopFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ShopFilter.Text.Length > 0)
            {
                this.txtFilter.IsReadOnly = true;
            }
            else
            {
                this.txtFilter.IsReadOnly = false;
            }

            CollectionViewSource.GetDefaultView(this.searchList.ItemsSource).Refresh();
        }
    }
}
