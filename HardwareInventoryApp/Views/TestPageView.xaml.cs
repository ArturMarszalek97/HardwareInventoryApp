using HardwareInventoryApp.Helpers;
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

namespace HardwareInventoryApp.Views
{
    /// <summary>
    /// Interaction logic for TestPageView.xaml
    /// </summary>
    public partial class TestPageView : Page
    {
        public TestPageView()
        {
            InitializeComponent();

            DataContext = new ItemPieViewModel();
        }

        internal class ItemPieViewModel
        {
            public List<ItemPie> List { get; private set; }

            public ItemPieViewModel()
            {
                List = new List<ItemPie>();
                this.PrepareList();
            }

            private void PrepareList()
            {
                List<string> categories = new List<string>();

                foreach (var item in Data.Items)
                {
                    if (!categories.Contains(item.Category))
                    {
                        categories.Add(item.Category);
                        this.List.Add(new ItemPie() { Category = item.Category, TotalCost = 0 });
                    }
                }

                foreach (var category in categories)
                {
                    foreach (var item in Data.Items)
                    {
                        if (item.Category == category)
                        {
                            this.List.Where(x => x.Category == category).Single().TotalCost += item.Price;
                        }
                    }
                }

                // prepare second part

                //List<string> months = new List<string>() { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" };
                //var currentMonth = DateTime.Now.Month;

                //for (int i = 0; i < 12; i++)
                //{
                //    if (currentMonth - 1 == 11)
                //    {
                //        this.List.Add(new ItemPie() { MonthToDisplay = months[i], Month = i + 1, TotalCostForMonth = 0 });
                //    }
                //    else
                //    {
                //        this.List.Add(new ItemPie() { MonthToDisplay = months[currentMonth + i], Month = currentMonth + 1, TotalCostForMonth = 0 });
                //    }
                //}

                //var tras = Data.Items.Where(x => x.DateOfPurchase > DateTime.Now.AddYears(-1)).ToList();

                //foreach (var statement in List)
                //{
                //    if (string.IsNullOrEmpty(statement.MonthToDisplay))
                //        continue;

                //    foreach (var item in tras)
                //    {
                //        if (statement.Month == item.DateOfPurchase.Month)
                //        {
                //            statement.TotalCostForMonth += item.Price;
                //        }
                //    }
                //}
            }
        }

        internal class ItemPie
        {
            public string Category { get; set; }
            public float TotalCost { get; set; }

            public ItemPie()
            {

            }
        }
    }
}
