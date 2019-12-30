using HardwareInventoryApp.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HardwareInventoryApp.Views
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : Window
    {
        public StatisticView()
        {
            InitializeComponent();

            ItemPie consumo = new ItemPie();
            DataContext = new ConsumoViewModel(/*consumo*/);
        }

        internal class ConsumoViewModel
        {
            public List<ItemPie> List { get; private set; }

            public ConsumoViewModel()
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
