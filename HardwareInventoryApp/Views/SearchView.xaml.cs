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
            if (string.IsNullOrEmpty(txtFilter.Text))
                return true;
            if (String.IsNullOrEmpty(txtFilter.Text) == false)
                return ((obj as Item).ItemName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) == 0);

            return false;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(this.searchList.ItemsSource).Refresh();
        }
    }
}
